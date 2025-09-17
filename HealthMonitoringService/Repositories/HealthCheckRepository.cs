using MovieDiscussionService_Contracts;
using MovieDiscussionService_Contracts.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieDiscussionService_HealthMonitoringService.Repositories
{
	public class HealthCheckRepository : IHealthCheckRepository
	{
		private List<HealthCheckRecord> _dummyData;

		public HealthCheckRepository()
		{
			// Placeholder: generišemo nekoliko lažnih podataka
			_dummyData = new List<HealthCheckRecord>
			{
				new HealthCheckRecord { Timestamp = DateTime.Now.AddMinutes(-10), ServiceName = "MovieDiscussionService", Status = "OK" },
				new HealthCheckRecord { Timestamp = DateTime.Now.AddMinutes(-5), ServiceName = "NotificationService", Status = "NOT_OK" },
				new HealthCheckRecord { Timestamp = DateTime.Now, ServiceName = "MovieDiscussionService", Status = "OK" }
			};
		}


		public IQueryable<HealthCheckRecord> GetLastTwoHours()
		{
			// Placeholder: vraćamo praznu listu
			return _dummyData.AsQueryable();
		}

		// Placeholder metoda za dodavanje zapisa (kasnije će worker upisivati u tabelu)
		public void AddRecord(HealthCheckRecord record)
		{
			_dummyData.Add(record);
		}
	}
}
