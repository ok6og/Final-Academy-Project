namespace MovieLibrary.Models.Requests.SubscriptionRequests
{
    public class UpdateSubscriptionRequest
    {
        public int SubscriptionId { get; set; }
        public int PlanId { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ValidTill { get; set; }
    }
}
