using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace MovieDiscussionService_WebRole.Controllers
{
	public class HealthController : Controller
	{
		[HttpGet]
		public ActionResult Index()
		{
			// Ako ovo radi, endpoint vraća čisti tekst "OK"
			return Content("OK", "text/plain");
		}
	}
}
