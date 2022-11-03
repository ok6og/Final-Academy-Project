using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibrary.Models.Requests.SubscriptionRequests
{
    public class UpdateSubscriptionRequest
    {
        public int SubscriptionsId { get; set; }
        public int PlanId { get; set; }
        public int UserId { get; set; }
        public DateTime SubscribedAtDate { get; set; }
        public DateTime SubscriptionValidTill { get; set; }
    }
}
