using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieDiscussionService_Data.Entities;

namespace MovieDiscussionService_Data.Interfaces
{
	public interface IUserRepository
	{
		IQueryable<User> RetrieveAllUsers();
		User GetUserByEmail(string email);
		void AddUser(User user);
		void UpdateUser(User user);
		void DeleteUser(string email);
	}
}

