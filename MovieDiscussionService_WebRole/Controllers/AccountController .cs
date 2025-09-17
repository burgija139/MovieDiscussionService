using System.Web.Mvc;
using MovieDiscussionService_Data.Repositories;
using MovieDiscussionService_Data;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using System.Web.Helpers;
using System.Web;


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
	}
}
