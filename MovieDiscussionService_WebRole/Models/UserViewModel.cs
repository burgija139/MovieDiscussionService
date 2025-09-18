using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MovieDiscussionService_WebRole.Models
{
	public class UserViewModel
	{
		public string Email { get; set; }
		public string Name { get; set; }
		public string Lastname { get; set; }
		public bool IsAdmin { get; set; }
		public bool IsVerified { get; set; }
	}
}