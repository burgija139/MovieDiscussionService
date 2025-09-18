using Microsoft.AspNetCore.Mvc;
using HealthStatusService_WebRole.Services;

namespace HealthStatusService_WebRole.Controllers
{
	public class HealthController : Controller
	{
		private readonly HealthMonitoringHttpService _healthService;

		public HealthController(HealthMonitoringHttpService healthService)
		{
			_healthService = healthService;
		}

		public async Task<IActionResult> Index()
		{
			var records = await _healthService.GetHealthRecordsAsync();
			return View(records);
		}

		public async Task<IActionResult> HealthStatusPartial()
		{
			var records = await _healthService.GetHealthRecordsAsync();
			return PartialView("_HealthStatusPartial", records);
		}
	}
}