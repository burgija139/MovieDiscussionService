using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;

namespace MovieDiscussionService_Data.Entities
{
	public class HealthCheckRecord : TableEntity
	{
		public string ServiceName { get; set; }
		public string Status { get; set; } // "OK" ili "NOT_OK"
		public DateTime CheckTime { get; set; }

		public HealthCheckRecord(string serviceName)
		{
			PartitionKey = serviceName;                  // Servis je particija
			RowKey = Guid.NewGuid().ToString();          // Jedinstveni ključ
			ServiceName = serviceName;
			CheckTime = DateTime.UtcNow;
		}

		public HealthCheckRecord() { } // obavezno za deserializaciju
	}
}

