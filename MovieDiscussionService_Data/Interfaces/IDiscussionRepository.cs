using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieDiscussionService_Data.Entities;

namespace MovieDiscussionService_Data.Interfaces
{
	public interface IDiscussionRepository
	{
		IQueryable<Discussion> RetrieveAllDiscussions();
		Discussion GetDiscussionById(string id);
		void AddDiscussion(Discussion discussion);
		void UpdateDiscussion(Discussion discussion);
		void DeleteDiscussion(string id);
	}
}

