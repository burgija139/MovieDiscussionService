using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieDiscussionService_Data.Entities;

namespace MovieDiscussionService_Data.Interfaces
{
	public interface IMovieRepository
	{
		IQueryable<Movie> RetrieveAllMovies();
		Movie GetMovieById(string id);
		void AddMovie(Movie movie);
		void UpdateMovie(Movie movie);
		void DeleteMovie(string id);
	}
}

