using System;
using System.Collections.Generic;
using System.Linq;
using MovieDiscussionService_Data.Entities;


namespace MovieDiscussionService_HealthMonitoringService.Proxies
{
	public interface IHealthMonitoringServiceProxy
	{
		List<HealthCheckRecord> GetLastTwoHours();
		void AddRecord(HealthCheckRecord record);
	}
}
