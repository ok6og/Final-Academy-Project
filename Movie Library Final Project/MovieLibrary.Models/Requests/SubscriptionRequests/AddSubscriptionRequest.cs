namespace MovieLibrary.Models.Requests.SubscriptionRequests
{
    public class AddSubscriptionRequest
    {
        public int PlanId { get; set; }
        public int UserId { get; set; }
    }
}
