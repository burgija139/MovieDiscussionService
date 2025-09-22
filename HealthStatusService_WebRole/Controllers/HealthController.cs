using System;
using Microsoft.Azure;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.WindowsAzure.Storage;
using MovieDiscussionService_Data.Entities;
using MovieDiscussionService_Data.Repositories;

namespace HealthStatusService_WebRole.Controllers
{
    public class HealthController : Controller
    {
        private readonly HealthCheckRepository _healthCheckRepo;

        public HealthController()
        {

            _healthCheckRepo = new HealthCheckRepository(CloudConfigurationManager.GetSetting("DataConnectionString"));
        }
        public ActionResult Index()
        {
            var records = _healthCheckRepo.GetLastTwoHours().ToList();
            return View(records);
        }

        public ActionResult HealthStatusPartial()
        {
            var records = _healthCheckRepo.GetLastTwoHours().ToList();
            return PartialView("_HealthStatusPartial", records);
        }
    }
}