using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using MovieDiscussionService_Data.Entities;
using MovieDiscussionService_Data.Interfaces;
using System;
using System.Linq;

namespace MovieDiscussionService_Data.Repositories
{
	public class DiscussionRepository : IDiscussionRepository
	{
		private CloudStorageAccount _storageAccount;
		private CloudTable _table;

		public DiscussionRepository()
		{
			_storageAccount = CloudStorageAccount.Parse(
				CloudConfigurationManager.GetSetting("DataConnectionString"));

			var tableClient = _storageAccount.CreateCloudTableClient();
			_table = tableClient.GetTableReference("DiscussionTable");
			_table.CreateIfNotExists();
		}

		public IQueryable<Discussion> RetrieveAllDiscussions()
		{
			var query = from d in _table.CreateQuery<Discussion>()
						where d.PartitionKey == "Discussion"
						select d;
			return query;
		}

		public void AddDiscussion(Discussion discussion)
		{
			TableOperation insertOp = TableOperation.Insert(discussion);
			_table.Execute(insertOp);
		}

		public Discussion GetDiscussionById(string id)
		{
			TableOperation retrieveOp = TableOperation.Retrieve<Discussion>("Discussion", id);
			var result = _table.Execute(retrieveOp);
			return result.Result as Discussion;
		}

		public void UpdateDiscussion(Discussion discussion)
		{
			TableOperation updateOp = TableOperation.Replace(discussion);
			_table.Execute(updateOp);
		}

		public void DeleteDiscussion(string id)
		{
			TableOperation retrieveOp = TableOperation.Retrieve<Discussion>("Discussion", id);
			var result = _table.Execute(retrieveOp);
			var discussion = result.Result as Discussion;
			if (discussion != null)
			{
				TableOperation deleteOp = TableOperation.Delete(discussion);
				_table.Execute(deleteOp);
			}
		}
	}
}
