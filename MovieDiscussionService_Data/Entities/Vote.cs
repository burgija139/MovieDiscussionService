using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDiscussionService_Data.Entities
{
    public class Vote : TableEntity
    {
        public int Value { get; set; } // +1 ili -1
        public Vote(string discussionId, string userEmail, int value)
        {
            PartitionKey = discussionId;
            RowKey = userEmail;
            Value = value;
        }
        public Vote() { }
    }
}
