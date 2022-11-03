using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibrary.Models.Responses
{
    public class IndividualHealthCheckResponse
    {
        public string Status { get; init; }
        public string Component { get; init; }
        public string Description { get; init; }
    }
}
