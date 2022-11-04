using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieLibrary.Models.Models;

namespace MovieLibrary.Models.Responses
{
    public record SubscriptionResponse
    {
        public int SubscriptionId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ValidTill { get; set; }
        public Plan Plan { get; set; }
        public User User { get; set; }
    }
}
