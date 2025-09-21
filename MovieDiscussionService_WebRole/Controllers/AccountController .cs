using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using MovieDiscussionService_Data;
using MovieDiscussionService_Data.Repositories;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;


namespace MovieDiscussionService.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserDataRepository _userRepo = new UserDataRepository();

		// GET: /Account/Login
		public ActionResult Login()
		{
			return View("Login");
		}

		// GET: /Account/Register
		public ActionResult Register()
		{
			return View("Register");
		}

        [HttpGet]
        public ActionResult EditProfile()
        {
            var email = Session["UserEmail"] as string;
            if (string.IsNullOrEmpty(email))
                return RedirectToAction("Login");

            var user = _userRepo.GetUserByEmail(email);
            if (user == null)
                return RedirectToAction("Login");

            return View("EditProfile", user);
        }

        // GET: /Account/Logout
        public ActionResult Logout()
		{
			Session.Clear();
			return RedirectToAction("Login");
		}

		// POST: /Account/Login
		[HttpPost]
		public ActionResult Login(string email, string password)
		{
			var user = _userRepo.GetUserByEmail(email);
			if (user == null)
			{
				ViewBag.Error = "User not found";
				return View();
			}

			bool valid = Crypto.VerifyHashedPassword(user.PasswordHash, password);
			if (!valid)
			{
				ViewBag.Error = "Invalid password";
				return View();
			}

			// Čuvamo samo RowKey u Session
			Session["UserEmail"] = user.RowKey;

			// Provera da li je admin
			if (user.IsAdmin)
			{
				return Redirect("https://localhost:7254/");
			}

			return RedirectToAction("Index", "App");
		}

		// POST: /Account/Register
		[HttpPost]
		public ActionResult Register(string name, string lastname, string country, string city, string address,
							 string email, string password, string confirmPassword, string gender,
							 HttpPostedFileBase profileImage)
		{
			//1. Da li su lozinke iste
			if (password != confirmPassword)
			{
				ViewBag.Error = "Passwords do not match.";
				return View();
			}

			//2.Da li user vec postoji
			if (_userRepo.GetUserByEmail(email) != null)
			{
				ViewBag.Error = "A user with this email already exists.";
				return View();
			}

			//3.Uploadujuj sliku na blob
			string imageUrl = "";
			if (profileImage != null && profileImage.ContentLength > 0)
			{
				string uniqueBlobName = $"user_{email}";
				var storageAccount = CloudStorageAccount.Parse(
					CloudConfigurationManager.GetSetting("DataConnectionString"));
				var blobClient = storageAccount.CreateCloudBlobClient();
				var container = blobClient.GetContainerReference("userimages");
				container.CreateIfNotExists();
				var blockBlob = container.GetBlockBlobReference(uniqueBlobName);
				blockBlob.UploadFromStream(profileImage.InputStream);
				imageUrl = blockBlob.Uri.ToString();
			}

			//4.Hashuj sifru
			string hashedPassword = Crypto.HashPassword(password);

			//5.Kreiraj usera i sacuvaj
			var user = new User(email)
			{
				Name = name,
				Lastname = lastname,
				Country = country,
				City = city,
				Address = address,
				PasswordHash = hashedPassword,
				Gender = gender,
				ImageUrl = imageUrl
			};

			_userRepo.AddUser(user);

			return RedirectToAction("Login");
		}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile(
            string Name,
            string Lastname,
            string Gender,
            string Country,
            string City,
            string Address,
            string CurrentPassword,
            string NewPassword,
            string ConfirmNewPassword,
            HttpPostedFileBase profileImage
        )
        {
            var email = Session["UserEmail"] as string;
            if (string.IsNullOrEmpty(email))
                return RedirectToAction("Login");

            var user = _userRepo.GetUserByEmail(email);
            if (user == null)
                return RedirectToAction("Login");

            // 1) Ako se menja lozinka — validacije
            if (!string.IsNullOrWhiteSpace(NewPassword) || !string.IsNullOrWhiteSpace(ConfirmNewPassword))
            {
                if (string.IsNullOrWhiteSpace(CurrentPassword))
                {
                    TempData["Error"] = "Unesite trenutnu lozinku kako biste postavili novu.";
                    return View("EditAccount", user);
                }
                // verifikuj trenutnu
                if (!Crypto.VerifyHashedPassword(user.PasswordHash, CurrentPassword))
                {
                    TempData["Error"] = "Trenutna lozinka nije ispravna.";
                    return View("EditAccount", user);
                }
                if (NewPassword != ConfirmNewPassword)
                {
                    TempData["Error"] = "Nova lozinka i potvrda se ne poklapaju.";
                    return View("EditAccount", user);
                }

                user.PasswordHash = Crypto.HashPassword(NewPassword);
            }

            // 2) Upload nove profil slike (opciono)
            if (profileImage != null && profileImage.ContentLength > 0)
            {
                var newUrl = UploadUserImage(profileImage, email);
                if (!string.IsNullOrEmpty(newUrl))
                {
                    user.ImageUrl = newUrl;
                }
            }

            // 3) Ažuriraj ostala polja (osim email-a)
            user.Name = Name;
            user.Lastname = Lastname;
            user.Gender = Gender;
            user.Country = Country;
            user.City = City;
            user.Address = Address;

            // 4) Sačuvaj
            _userRepo.UpdateUser(user);

            TempData["Success"] = "Profil uspešno ažuriran.";
            return RedirectToAction("EditProfile");
        }

        private string UploadUserImage(HttpPostedFileBase file, string email)
        {
            if (file == null || file.ContentLength == 0) return null;

            var storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("DataConnectionString"));
            var blobClient = storageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference("userimages");
            container.CreateIfNotExists();
            container.SetPermissions(new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Blob
            });

            // Jedinstveno ime (email + ticks + ekstenzija)
            var ext = System.IO.Path.GetExtension(file.FileName);
            if (string.IsNullOrEmpty(ext)) ext = ".jpg";
            string safeEmail = string.Concat(email.Where(ch => char.IsLetterOrDigit(ch) || ch == '-' || ch == '_'));
            string blobName = $"user_{safeEmail}_{System.DateTime.UtcNow.Ticks}{ext}";

            var blockBlob = container.GetBlockBlobReference(blobName);
            blockBlob.Properties.ContentType = file.ContentType ?? "image/jpeg";
            using (var stream = file.InputStream)
            {
                blockBlob.UploadFromStream(stream);
            }
            return blockBlob.Uri.ToString();
        }

    }
}
