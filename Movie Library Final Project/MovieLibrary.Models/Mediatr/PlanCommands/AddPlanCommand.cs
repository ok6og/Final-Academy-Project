using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MovieLibrary.Models.Models;
using MovieLibrary.Models.Requests.PlanRequests;

namespace MovieLibrary.Models.Mediatr.PlanCommands
{
    public record AddPlanCommand(AddPlanRequest plan) : IRequest<Plan>
    {
    }
}
