using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDiscussionService_Data.Entities
{
	public class Discussion : TableEntity
	{
		public string MovieTitle { get; set; } // Veza ka filmu
		public string AuthorEmail { get; set; } // Ko je pokrenuo diskusiju
		public string Title { get; set; }
		public string Content { get; set; }
		public DateTime CreatedAt { get; set; }

		public Discussion(string id)
		{
			PartitionKey = "Discussion";
			RowKey = id; // može GUID ili kombinacija MovieTitle+Timestamp
		}

		public Discussion() { } // default za deserializaciju
	}
}
