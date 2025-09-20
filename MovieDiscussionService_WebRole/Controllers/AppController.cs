using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using MovieDiscussionService_Data;
using MovieDiscussionService_Data.Entities;
using MovieDiscussionService_Data.Repositories;
using MovieDiscussionService_WebRole.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MovieDiscussionService_WebRole.Controllers
{
    public class AppController : Controller
    {
        // postojeći repo-i iz tvog data sloja
        private readonly DiscussionRepository _discussionRepo = new DiscussionRepository();
        private readonly MovieRepository _movieRepo = new MovieRepository();
        private readonly UserDataRepository _userRepo = new UserDataRepository();

        // dodatni Table/Queue resursi
        private readonly CloudStorageAccount _storageAccount;
        private readonly CommentRepository _commentRepo;
        private readonly FollowRepository _followRepo;
        private readonly VoteRepository _voteRepo;
        private readonly CloudQueue _notificationQueue;

        public AppController()
        {
            _storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("DataConnectionString"));

            _commentRepo = new CommentRepository(_storageAccount);
            _followRepo = new FollowRepository(_storageAccount);
            _voteRepo = new VoteRepository(_storageAccount);

            // Queue za slanje email notifikacija (obrađuje ga Worker Role)
            var queueClient = _storageAccount.CreateCloudQueueClient();
            _notificationQueue = queueClient.GetQueueReference("notificationqueue");
            _notificationQueue.CreateIfNotExists();
        }

        // Utility
        private string CurrentUserEmail => Session["UserEmail"] as string;

        private User CurrentUserOrNull()
        {
            var email = CurrentUserEmail;
            return email == null ? null : _userRepo.GetUserByEmail(email);
        }

        private bool EnsureAuthenticated(out User u)
        {
            u = CurrentUserOrNull();
            if (u == null)
            {
                return false;
            }
            return true;
        }

        // GET: /App/Index?page=1&pageSize=10
        // Lista svih diskusija sa paginacijom
        public ActionResult Index(int page = 1, int pageSize = 10)
        {
            var user = CurrentUserOrNull();
            ViewBag.User = user;

            var all = _discussionRepo
                .RetrieveAllDiscussions()
                .AsEnumerable()
                .OrderByDescending(d => d.CreatedAt)
                .ToList(); // Table LINQ skip/take nije uvek server-side; odradimo u memoriji

            int total = all.Count;
            var items = all.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;
            ViewBag.Total = total;
            ViewBag.TotalPages = (int)Math.Ceiling(total / (double)pageSize);

            return View("Home", items); // Home.cshtml će prikazati listu
        }

        // GET: /App/Details/{id}
        // Detalji diskusije, komentari, status praćenja, mogućnosti interakcije
        public ActionResult Details(string id)
        {
            var discussion = _discussionRepo.GetDiscussionById(id);
            if (discussion == null) return HttpNotFound();

            var comments = _commentRepo.GetByDiscussion(id)
                                       .AsEnumerable()
                                       .OrderByDescending(c => c.CreatedAt)
                                       .ToList();

            var user = CurrentUserOrNull();
            bool isAuthor = user != null && user.RowKey == discussion.AuthorEmail;
            bool isFollowing = user != null && _followRepo.IsFollowing(id, user.RowKey);

            ViewBag.IsAuthor = isAuthor;
            ViewBag.IsFollowing = isFollowing;
            ViewBag.User = user;

            return View("Details", new DiscussionDetailsVm
            {
                Discussion = discussion,
                Movie = _movieRepo.GetMovieByTitle(discussion.MovieTitle),
                Comments = comments
            });
        }

        // GET: /App/New
        // Forma za kreiranje nove diskusije (samo verifikovan autor)
        public ActionResult New()
        {
            if (!EnsureAuthenticated(out var user)) return RedirectToAction("Login", "Account");
            if (!user.IsVerified)
            {
                TempData["Error"] = "Samo verifikovani autori mogu pokrenuti novu diskusiju.";
                return RedirectToAction("Index");
            }
            return View("New");
        }

        // POST: /App/Create
        // Kreiranje i filma (ako ne postoji) i diskusije
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            string MovieTitle,
            int Year,
            string Genre,
            double ImdbRating,
            string Synopsis,
            string Duration,
            string PosterUrl,
            string Title // naslov diskusije
        )
        {
            if (!EnsureAuthenticated(out var user)) return RedirectToAction("Login", "Account");
            if (!user.IsVerified)
            {
                TempData["Error"] = "Samo verifikovani autori mogu pokrenuti novu diskusiju.";
                return RedirectToAction("Index");
            }

            // 1) Upsert filma
            var existingMovie = _movieRepo.GetMovieByTitle(MovieTitle);
            if (existingMovie == null)
            {
                _movieRepo.AddMovie(new Movie(MovieTitle)
                {
                    Title = MovieTitle,
                    Year = Year,
                    Genre = Genre,
                    ImdbRating = ImdbRating,
                    Synopsis = Synopsis,
                    Duration = Duration,
                    PosterUrl = PosterUrl
                });
            }
            else
            {
                // opciono osveži meta
                existingMovie.Year = Year;
                existingMovie.Genre = Genre;
                existingMovie.ImdbRating = ImdbRating;
                existingMovie.Synopsis = Synopsis;
                existingMovie.Duration = Duration;
                existingMovie.PosterUrl = PosterUrl;
                _movieRepo.UpdateMovie(existingMovie);
            }

            // 2) Kreiraj diskusiju
            string discussionId = Guid.NewGuid().ToString("N");
            var discussion = new Discussion(discussionId)
            {
                MovieTitle = MovieTitle,
                AuthorEmail = user.RowKey,
                Title = Title,
                CreatedAt = DateTime.UtcNow,
                PositiveCount = 0,
                NegativeCount = 0,
                CommentCount = 0,
                //LastCommentAuthor = null,
                //LastCommentAt = null
            };
            _discussionRepo.AddDiscussion(discussion);

            TempData["Success"] = "Diskusija je uspešno kreirana.";
            return RedirectToAction("Details", new { id = discussionId });
        }

        // GET: /App/Edit/{id}
        public ActionResult Edit(string id)
        {
            if (!EnsureAuthenticated(out var user)) return RedirectToAction("Login", "Account");
            var d = _discussionRepo.GetDiscussionById(id);
            if (d == null) return HttpNotFound();
            if (d.AuthorEmail != user.RowKey) return new HttpUnauthorizedResult();

            var m = _movieRepo.GetMovieByTitle(d.MovieTitle);
            return View("Edit", new EditDiscussionVm { Discussion = d, Movie = m });
        }

        // POST: /App/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, string Title, string Synopsis, double ImdbRating, string Genre, int Year, string Duration, string PosterUrl)
        {
            if (!EnsureAuthenticated(out var user)) return RedirectToAction("Login", "Account");
            var d = _discussionRepo.GetDiscussionById(id);
            if (d == null) return HttpNotFound();
            if (d.AuthorEmail != user.RowKey) return new HttpUnauthorizedResult();

            d.Title = Title;
            _discussionRepo.UpdateDiscussion(d);

            var m = _movieRepo.GetMovieByTitle(d.MovieTitle);
            if (m != null)
            {
                m.Synopsis = Synopsis;
                m.ImdbRating = ImdbRating;
                m.Genre = Genre;
                m.Year = Year;
                m.Duration = Duration;
                m.PosterUrl = PosterUrl;
                _movieRepo.UpdateMovie(m);
            }

            TempData["Success"] = "Diskusija je izmenjena.";
            return RedirectToAction("Details", new { id });
        }

        // POST: /App/Delete/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id)
        {
            if (!EnsureAuthenticated(out var user)) return RedirectToAction("Login", "Account");
            var d = _discussionRepo.GetDiscussionById(id);
            if (d == null) return HttpNotFound();
            if (d.AuthorEmail != user.RowKey) return new HttpUnauthorizedResult();

            _discussionRepo.DeleteDiscussion(id);
            TempData["Success"] = "Diskusija je obrisana.";
            return RedirectToAction("Index");
        }

        // POST: /App/Vote/{id}?value=1  ili -1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Vote(string id, int value)
        {
            if (!EnsureAuthenticated(out var user)) return RedirectToAction("Login", "Account");
            var d = _discussionRepo.GetDiscussionById(id);
            if (d == null) return HttpNotFound();
            if (d.AuthorEmail == user.RowKey)
            {
                TempData["Error"] = "Autor ne može glasati za sopstvenu diskusiju.";
                return RedirectToAction("Details", new { id });
            }
            if (value != 1 && value != -1)
            {
                TempData["Error"] = "Nevažeći glas.";
                return RedirectToAction("Details", new { id });
            }

            var existing = _voteRepo.Get(id, user.RowKey);
            if (existing == null)
            {
                // novi glas
                if (value == 1) d.PositiveCount++;
                else d.NegativeCount++;

                _voteRepo.Upsert(new Vote(id, user.RowKey, value));
                _discussionRepo.UpdateDiscussion(d);
            }
            else
            {
                if (existing.Value == value)
                {
                    // nema promene
                }
                else
                {
                    // promena glasa: ažuriraj brojace
                    if (existing.Value == 1) { d.PositiveCount--; d.NegativeCount++; }
                    else { d.NegativeCount--; d.PositiveCount++; }

                    existing.Value = value;
                    _voteRepo.Upsert(existing);
                    _discussionRepo.UpdateDiscussion(d);
                }
            }

            return RedirectToAction("Details", new { id });
        }

        // POST: /App/Follow/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Follow(string id)
        {
            if (!EnsureAuthenticated(out var user)) return RedirectToAction("Login", "Account");
            var d = _discussionRepo.GetDiscussionById(id);
            if (d == null) return HttpNotFound();

            _followRepo.Follow(id, user.RowKey);
            TempData["Success"] = "Pratiš ovu diskusiju.";
            return RedirectToAction("Details", new { id });
        }

        // POST: /App/Unfollow/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Unfollow(string id)
        {
            if (!EnsureAuthenticated(out var user)) return RedirectToAction("Login", "Account");
            var d = _discussionRepo.GetDiscussionById(id);
            if (d == null) return HttpNotFound();

            _followRepo.Unfollow(id, user.RowKey);
            TempData["Success"] = "Više ne pratiš ovu diskusiju.";
            return RedirectToAction("Details", new { id });
        }

        // POST: /App/Comment/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Comment(string id, string text)
        {
            if (!EnsureAuthenticated(out var user))
                return RedirectToAction("Login", "Account");

            var d = _discussionRepo.GetDiscussionById(id);
            if (d == null)
                return HttpNotFound();

            var c = new Comment(id)
            {
                Text = text,
                AuthorEmail = user.RowKey,
                CreatedAt = DateTime.UtcNow
            };
            _commentRepo.Add(c);

            // ažuriraj meta podatke u diskusiji
            d.CommentCount += 1;
            _discussionRepo.UpdateDiscussion(d);

            // enqueue poruka u "notifications"
            var queueClient = _storageAccount.CreateCloudQueueClient();
            var queue = queueClient.GetQueueReference("notifications");
            await queue.CreateIfNotExistsAsync();
            await queue.AddMessageAsync(new CloudQueueMessage($"{c.PartitionKey}|{c.RowKey}"));


            TempData["Success"] = "Komentar je dodat.";
            return RedirectToAction("Details", new { id });
        }

        // Enqueue poruka za slanje emailova pratiocima
        /*private void NotifyFollowersAsync(string discussionId, string discussionTitle, Comment comment)
        {
            var followers = _followRepo.GetFollowers(discussionId)
                                       .Where(email => !string.Equals(email, comment.AuthorEmail, StringComparison.OrdinalIgnoreCase))
                                       .ToList();
            if (!followers.Any()) return;

            var payload = new NotificationPayload
            {
                DiscussionId = discussionId,
                DiscussionTitle = discussionTitle,
                CommentAuthor = comment.AuthorEmail,
                CommentText = comment.Text,
                CreatedAtUtc = comment.CreatedAt,
                Recipients = followers
            };

            // Serijalizuj payload kao JSON (prosto)
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(payload);
            var msg = new CloudQueueMessage(json);
            _notificationQueue.AddMessage(msg);
        }*/
    }
}
