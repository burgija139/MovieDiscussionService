using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDiscussionService_Contracts.Contracts
{
	public interface INotificationService
	{
		void SendNotification(string commentId);
	}
}
