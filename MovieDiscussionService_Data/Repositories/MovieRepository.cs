using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using MovieDiscussionService_Data.Entities;
using System;
using System.Linq;

namespace MovieDiscussionService_Data.Repositories
{
	public class MovieRepository
	{
		private CloudStorageAccount _storageAccount;
		private CloudTable _table;

		public MovieRepository()
		{
			_storageAccount = CloudStorageAccount.Parse(
				CloudConfigurationManager.GetSetting("DataConnectionString"));

			var tableClient = _storageAccount.CreateCloudTableClient();
			_table = tableClient.GetTableReference("MovieTable");
			_table.CreateIfNotExists();
		}

		public IQueryable<Movie> RetrieveAllMovies()
		{
			var query = from m in _table.CreateQuery<Movie>()
						where m.PartitionKey == "Movie"
						select m;
			return query;
		}

		public void AddMovie(Movie movie)
		{
			TableOperation insertOp = TableOperation.Insert(movie);
			_table.Execute(insertOp);
		}

		public Movie GetMovieByTitle(string title)
		{
			TableOperation retrieveOp = TableOperation.Retrieve<Movie>("Movie", title);
			var result = _table.Execute(retrieveOp);
			return result.Result as Movie;
		}

		public void UpdateMovie(Movie movie)
		{
			TableOperation updateOp = TableOperation.Replace(movie);
			_table.Execute(updateOp);
		}

		public void DeleteMovie(string title)
		{
			TableOperation retrieveOp = TableOperation.Retrieve<Movie>("Movie", title);
			var result = _table.Execute(retrieveOp);
			var movie = result.Result as Movie;
			if (movie != null)
			{
				TableOperation deleteOp = TableOperation.Delete(movie);
				_table.Execute(deleteOp);
			}
		}
	}
}
