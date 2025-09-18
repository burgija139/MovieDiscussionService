namespace HealthStatusService_WebRole.Models
{
	public class HealthRecord
	{
		public string ServiceName { get; set; }
		public string Status { get; set; }
		public DateTime CheckTime { get; set; }
	}
}