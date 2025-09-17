using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDiscussionService_Contracts.Contracts
{
	public class HealthCheckRecord
	{
		public DateTime Timestamp { get; set; }
		public string ServiceName { get; set; }
		public string Status { get; set; } // "OK" ili "NOT_OK"
	}
}
