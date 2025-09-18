using HealthStatusService_WebRole.Models;
using System.Text.Json;

namespace HealthStatusService_WebRole.Services
{
	public class HealthMonitoringHttpService
	{
		private readonly HttpClient _httpClient;

		public HealthMonitoringHttpService(HttpClient httpClient)
		{
			_httpClient = httpClient;
			_httpClient.BaseAddress = new Uri("http://localhost:50002/");
		}

		public async Task<List<HealthRecord>> GetHealthRecordsAsync()
		{
			try
			{
				var response = await _httpClient.GetAsync("/health-monitoring");
				response.EnsureSuccessStatusCode();

				var json = await response.Content.ReadAsStringAsync();
				var records = JsonSerializer.Deserialize<List<HealthRecord>>(json);
				return records ?? new List<HealthRecord>();
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error fetching health records: {ex.Message}");
				return new List<HealthRecord>();
			}
		}
	}
}
