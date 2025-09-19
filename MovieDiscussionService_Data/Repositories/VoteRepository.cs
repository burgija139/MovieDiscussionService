using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using MovieDiscussionService_Data.Entities;
using MovieDiscussionService_Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDiscussionService_Data.Repositories
{
    public class VoteRepository : IVoteRepository
    {
        private readonly CloudTable _table;
        public VoteRepository(CloudStorageAccount acc)
        {
            var client = acc.CreateCloudTableClient();
            _table = client.GetTableReference("VoteTable");
            _table.CreateIfNotExists();
        }

        public Vote Get(string discussionId, string userEmail)
        {
            return _table.Execute(TableOperation.Retrieve<Vote>(discussionId, userEmail)).Result as Vote;
        }

        public void Upsert(Vote v)
        {
            _table.Execute(TableOperation.InsertOrReplace(v));
        }
    }
}
