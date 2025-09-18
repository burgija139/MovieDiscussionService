using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieDiscussionService_Data.Entities;

namespace MovieDiscussionService_Data.Interfaces
{
	public interface IHealthCheckRepository
	{
		// Placeholder metoda: vraća poslednja 2 sata podataka
		IQueryable<HealthCheckRecord> GetLastTwoHours();
	}
}
