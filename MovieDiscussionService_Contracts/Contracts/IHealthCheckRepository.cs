using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDiscussionService_Contracts.Contracts
{
	public interface IHealthCheckRepository
	{
		// Placeholder metoda: vraća poslednja 2 sata podataka
		IQueryable<HealthCheckRecord> GetLastTwoHours();
	}
}
