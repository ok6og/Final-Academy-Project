using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessagePack;

namespace MovieLibrary.Models.Models
{
    [MessagePackObject]
    public record Subscription
    {
        [Key(0)]
        public int SubscriptionId { get; set; }
        [Key(1)]
        public int PlanId { get; set; }
        [Key(2)]
        public int UserId { get; set; }
        [Key(3)]
        public DateTime CreatedAt { get; set; }
        [Key(4)]
        public DateTime ValidTill { get; set; }
    }
}
