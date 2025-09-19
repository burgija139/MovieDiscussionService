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
    public class CommentRepository : ICommentRepository
    {
        private readonly CloudTable _table;
        public CommentRepository(CloudStorageAccount acc)
        {
            var client = acc.CreateCloudTableClient();
            _table = client.GetTableReference("CommentTable");
            _table.CreateIfNotExists();
        }

        public IQueryable<Comment> GetByDiscussion(string discussionId)
        {
            return from c in _table.CreateQuery<Comment>()
                   where c.PartitionKey == discussionId
                   select c;
        }

        public void Add(Comment c) => _table.Execute(TableOperation.Insert(c));
    }

    

}
