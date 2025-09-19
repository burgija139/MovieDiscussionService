using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDiscussionService_Data.Entities
{
    public class Follow : TableEntity
    {
        public Follow(string discussionId, string userEmail)
        {
            PartitionKey = discussionId;
            RowKey = userEmail;
        }
        public Follow() { }
    }
}
