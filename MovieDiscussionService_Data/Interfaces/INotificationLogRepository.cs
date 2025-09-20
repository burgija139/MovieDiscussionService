using MovieDiscussionService_Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDiscussionService_Data.Interfaces
{
    public interface INotificationLogRepository
    {
        void Add(NotificationLog log);
    }
}
