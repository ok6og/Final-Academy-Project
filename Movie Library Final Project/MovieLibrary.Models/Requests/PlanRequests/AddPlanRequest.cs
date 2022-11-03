using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibrary.Models.Requests.PlanRequests
{
    public class AddPlanRequest
    {
        public string Type { get; set; }
        public int PricePerMonth { get; set; }
    }
}
