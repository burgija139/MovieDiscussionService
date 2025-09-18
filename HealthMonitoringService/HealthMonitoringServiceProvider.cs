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
		}

		public List<HealthCheckRecord> GetLastTwoHours()
		{
			return _repo.GetLastTwoHours().ToList();
		}

		public void AddRecord(HealthCheckRecord record)
		{
			_repo.AddRecordAsync(record).Wait();
		}
	}
}
