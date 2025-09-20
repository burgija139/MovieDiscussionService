using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDiscussionService_Data.Entities
{
    public class NotificationLog : TableEntity
    {
        public string CommentId { get; set; }
        public DateTime DateSent { get; set; }
        public int SentCount { get; set; }

        public NotificationLog()
        {
            PartitionKey = "Notification";
            RowKey = Guid.NewGuid().ToString();
        }
    }
}
