using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MovieLibrary.Models.Models;
using MovieLibrary.Models.Responses;

namespace MovieLibrary.Models.Mediatr.SubscriptionCommands
{
    public record GetSubscriptionByIdCommand(int subId) : IRequest<HttpResponse<Subscription>>
    {
    }
}
