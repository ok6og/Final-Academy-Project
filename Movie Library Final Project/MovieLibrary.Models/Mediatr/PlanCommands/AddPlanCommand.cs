﻿using MediatR;
using MovieLibrary.Models.Models;
using MovieLibrary.Models.Requests.PlanRequests;
using MovieLibrary.Models.Responses;

namespace MovieLibrary.Models.Mediatr.PlanCommands
{
    public record AddPlanCommand(AddPlanRequest plan) : IRequest<HttpResponse<Plan>>
    {
    }
}
