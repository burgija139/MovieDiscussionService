using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IAlertEmailRepository
{
	Task AddAlertEmailAsync(string email);
	Task RemoveAlertEmailAsync(string email);
	List<string> GetAllAlertEmails();
}
