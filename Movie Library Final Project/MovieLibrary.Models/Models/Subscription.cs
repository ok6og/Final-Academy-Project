using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibrary.Models.Models
{
    public record Subscription
    {
        public int SubscriptionsId { get; set; }
        public int PlanId { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ValidTill { get; set; }
    }
}
