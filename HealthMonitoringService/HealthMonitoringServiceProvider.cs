using MovieDiscussionService_HealthMonitoringService.Proxies;
using System.Collections.Generic;
using System.Linq;
using MovieDiscussionService_Data.Entities;
using MovieDiscussionService_Data.Repositories;

namespace MovieDiscussionService_HealthMonitoringService
{
	public class HealthMonitoringServiceProvider : IHealthMonitoringServiceProxy
	{
		private readonly HealthCheckRepository _repo;

		public HealthMonitoringServiceProvider(string connectionString)
		{
			_repo = new HealthCheckRepository(connectionString);
			_alertEmailRepo = new AlertEmailRepository(connectionString);
		}

		public List<HealthCheckRecord> GetLastTwoHours()
		{
			return _repo.GetLastTwoHours().ToList();
		}

		public void AddRecord(HealthCheckRecord record)
		{
			_repo.AddRecordAsync(record).Wait();
		}

		private readonly IAlertEmailRepository _alertEmailRepo;

		public List<string> GetAlertEmails()
		{
			return _alertEmailRepo.GetAllAlertEmails();
		}

		public void AddAlertEmail(string email)
		{
			_alertEmailRepo.AddAlertEmailAsync(email).Wait();
		}

		public void RemoveAlertEmail(string email)
		{
			_alertEmailRepo.RemoveAlertEmailAsync(email).Wait();
		}
	}
}
