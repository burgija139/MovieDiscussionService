using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDiscussionService_Data.Entities
{
	public class Movie : TableEntity
	{
		public string Title { get; set; }
		public int Year { get; set; }
		public string Genre { get; set; }
		public double ImdbRating { get; set; }
		public string Synopsis { get; set; }
		public string Duration { get; set; }
		public string PosterUrl { get; set; }

		public Movie(string title)
		{
			PartitionKey = "Movie";
			RowKey = title; // ili neki jedinstveni ID
		}

		public Movie() { } // default za deserializaciju
	}
}
