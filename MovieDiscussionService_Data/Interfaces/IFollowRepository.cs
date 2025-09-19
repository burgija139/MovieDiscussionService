using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDiscussionService_Data.Interfaces
{
    public interface IFollowRepository
    {
        bool IsFollowing(string discussionId, string userEmail);

        void Follow(string discussionId, string userEmail);

        void Unfollow(string discussionId, string userEmail);

        IEnumerable<string> GetFollowers(string discussionId);
    }
}
