using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class AlertEmailRepository : IAlertEmailRepository
{
	private readonly CloudTable _table;

	public AlertEmailRepository(string connectionString)
	{
		var account = CloudStorageAccount.Parse(connectionString);
		var client = account.CreateCloudTableClient();
		_table = client.GetTableReference("AlertEmails");
		_table.CreateIfNotExistsAsync().Wait();
	}

	public async Task AddAlertEmailAsync(string email)
	{
		var alertEmail = new AlertEmail(email);
		var insert = TableOperation.Insert(alertEmail);
		await _table.ExecuteAsync(insert);
	}

	public async Task RemoveAlertEmailAsync(string email)
	{
		var retrieve = TableOperation.Retrieve<AlertEmail>("AlertEmails", email);
		var result = await _table.ExecuteAsync(retrieve);

		if (result.Result != null)
		{
			var delete = TableOperation.Delete((AlertEmail)result.Result);
			await _table.ExecuteAsync(delete);
		}
	}

	public List<string> GetAllAlertEmails()
	{
		var query = new TableQuery<AlertEmail>()
			.Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "AlertEmails"));

		return _table.ExecuteQuery(query).Select(e => e.RowKey).ToList();
	}
}