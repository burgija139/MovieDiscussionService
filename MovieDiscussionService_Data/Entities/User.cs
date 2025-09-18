using System;
using Microsoft.WindowsAzure.Storage.Table;

namespace MovieDiscussionService_Data
{
	public class User : TableEntity
	{
		public string Name { get; set; }
		public string Lastname { get; set; }
		public string Gender { get; set; }
		public string Country { get; set; }
		public string City { get; set; }
		public string Address { get; set; }
		//email se vec cuva u RowKey pa nema potrebe za dupliranje
		public string PasswordHash { get; set; }
		public string ImageUrl { get; set; }
		public bool IsVerified { get; set; } = false; // Podrazumevano false
		public bool IsAdmin { get; set; } = false;
		public User(string email)
		{
			this.PartitionKey = "User";
			this.RowKey = email;
			this.IsVerified = false; // Podrazumevano
		}
		public User() { } // Default constructor for deserialization

	}

}
