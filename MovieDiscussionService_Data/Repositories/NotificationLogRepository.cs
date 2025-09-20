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
    public class NotificationLogRepository : INotificationLogRepository
    {
        private readonly CloudTable _table;
        public NotificationLogRepository(CloudStorageAccount acc)
        {
            var client = acc.CreateCloudTableClient();
            _table = client.GetTableReference("NotificationTable");
            _table.CreateIfNotExists();
        }

        public void Add(NotificationLog log) => _table.Execute(TableOperation.Insert(log));
    }
}
