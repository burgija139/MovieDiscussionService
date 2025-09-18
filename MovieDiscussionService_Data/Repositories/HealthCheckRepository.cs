using MovieDiscussionService_Data.Entities;
using MovieDiscussionService_Data.Interfaces;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Security.Permissions;

namespace MovieDiscussionService_Data.Repositories
{
	public class HealthCheckRepository : IHealthCheckRepository
	{
		private readonly CloudTable _table;

		public HealthCheckRepository(string connectionString)
		{
			var account = CloudStorageAccount.Parse(connectionString);
			var client = account.CreateCloudTableClient();
			_table = client.GetTableReference("HealthCheck");
			_table.CreateIfNotExistsAsync().Wait();
		}

		public async Task AddRecordAsync(HealthCheckRecord record)
		{
			var insert = TableOperation.Insert(record);
			await _table.ExecuteAsync(insert);
		}

		public IQueryable<HealthCheckRecord> GetLastTwoHours()
		{
			var cutoff = DateTime.UtcNow.AddHours(-2);
			var filter = TableQuery.GenerateFilterConditionForDate(
				"CheckTime",
				QueryComparisons.GreaterThanOrEqual,
				cutoff
			);

			var query = new TableQuery<HealthCheckRecord>().Where(filter);
			return _table.ExecuteQuery(query).AsQueryable();
		}
	}
}
