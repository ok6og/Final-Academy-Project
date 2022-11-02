using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibrary.Models.Models
{
    public record Subscriptions
    {
        public int SubscriptionsId { get; set; }
        public string Type { get; set; }
        public int PricePerMonth { get; set; }
        public DateOnly CreatedAt { get; set; }
        public DateOnly ValidTill { get; set; }
    }
}
