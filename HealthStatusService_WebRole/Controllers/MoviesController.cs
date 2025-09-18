using Microsoft.AspNetCore.Mvc;
using MovieDiscussionService_Data.Entities;

public class MoviesController : Controller
{
	private readonly IHttpClientFactory _httpClientFactory;

	public MoviesController(IHttpClientFactory httpClientFactory)
	{
		_httpClientFactory = httpClientFactory;
	}

	public async Task<IActionResult> Index()
	{
		var client = _httpClientFactory.CreateClient("MovieService");
		var response = await client.GetAsync("movies");

		if (!response.IsSuccessStatusCode)
		{
			return View("Error");
		}

		var data = await response.Content.ReadFromJsonAsync<List<Movie>>();
		return View(data);
	}
}
