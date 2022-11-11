using MediatR;
using MovieLibrary.Models.Models;
using MovieLibrary.Models.Responses;

namespace MovieLibrary.Models.Mediatr.PlanCommands
{
    public record DeletePlanCommand(int planId) : IRequest<HttpResponse<Plan>>
    {
    }
}
