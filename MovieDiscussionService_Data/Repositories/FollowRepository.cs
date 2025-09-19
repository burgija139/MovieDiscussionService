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
    public class FollowRepository : IFollowRepository
    {
        private readonly CloudTable _table;
        public FollowRepository(CloudStorageAccount acc)
        {
            var client = acc.CreateCloudTableClient();
            _table = client.GetTableReference("FollowTable");
            _table.CreateIfNotExists();
        }

        public bool IsFollowing(string discussionId, string userEmail)
        {
            var res = _table.Execute(TableOperation.Retrieve<Follow>(discussionId, userEmail)).Result as Follow;
            return res != null;
        }

        public void Follow(string discussionId, string userEmail)
        {
            if (!IsFollowing(discussionId, userEmail))
            {
                _table.Execute(TableOperation.Insert(new Follow(discussionId, userEmail)));
            }
        }

        public void Unfollow(string discussionId, string userEmail)
        {
            var f = _table.Execute(TableOperation.Retrieve<Follow>(discussionId, userEmail)).Result as Follow;
            if (f != null) _table.Execute(TableOperation.Delete(f));
        }

        public IEnumerable<string> GetFollowers(string discussionId)
        {
            var q = from f in _table.CreateQuery<Follow>()
                    where f.PartitionKey == discussionId
                    select f.RowKey; // userEmail
            return q.ToList();
        }
    }
}
