using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class AlertEmail : TableEntity
{
	public AlertEmail(string email)
	{
		PartitionKey = "AlertEmails";
		RowKey = email;
	}

	public AlertEmail() { }

	public string Email { get; set; }
}
