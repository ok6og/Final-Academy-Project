namespace MovieLibrary.Models.Responses
{
    public class HealthCheckResponse
    {
        public string Status { get; set; }
        public IEnumerable<IndividualHealthCheckResponse> HealthCheckList { get; set; }
        public TimeSpan HealthCheckDuration { get; set; }
    }
}
