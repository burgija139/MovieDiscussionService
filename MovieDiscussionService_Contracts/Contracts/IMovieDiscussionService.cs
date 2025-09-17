using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDiscussionService_Contracts.Contracts
{
	public interface IMovieDiscussionService
	{
		void StartDiscussion(string movieId, string userId);
	}
}
