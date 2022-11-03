using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibrary.Models.Models
{
    public record Plan
    {
        public int PlanId { get; set; }
        public string Type { get; set; }
        public int PricePerMonth { get; set; }
    }
}
