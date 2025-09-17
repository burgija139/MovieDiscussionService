using HealthStatusService_WebRole.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using MovieDiscussionService_Contracts.Contracts;
using MovieDiscussionService_HealthMonitoringService.Repositories;

namespace HealthStatusService_WebRole.Controllers
{
	public class HealthController : Controller
	{
		private readonly IHealthCheckRepository _repo;

		public HealthController()
		{
			// Placeholder: direktno kreiramo repository
			_repo = new HealthCheckRepository();
		}

		// GET: Health/Index
		public IActionResult Index()
		{
			var data = _repo.GetLastTwoHours(); // kasnije čita iz tabele
			return View(data); // View će prikazati listu / grafikon
		}
	}
}