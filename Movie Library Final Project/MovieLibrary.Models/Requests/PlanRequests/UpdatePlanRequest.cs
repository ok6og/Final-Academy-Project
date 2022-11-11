namespace MovieLibrary.Models.Requests.PlanRequests
{
    public class UpdatePlanRequest
    {
        public int PlanId { get; set; }
        public string Type { get; set; }
        public int PricePerMonth { get; set; }
    }
}
