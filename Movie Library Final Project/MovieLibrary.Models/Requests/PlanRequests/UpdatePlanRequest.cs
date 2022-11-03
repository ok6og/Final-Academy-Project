using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibrary.Models.Requests.PlanRequests
{
    public class UpdatePlanRequest
    {
        public int PlanId { get; set; }
        public string Type { get; set; }
        public int PricePerMonth { get; set; }
    }
}
