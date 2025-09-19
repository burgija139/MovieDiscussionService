using MovieDiscussionService_Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDiscussionService_Data.Interfaces
{
    public interface IVoteRepository
    {
        Vote Get(string discussionId, string userEmail);

        void Upsert(Vote v);
    }
}
