using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MovieLibrary.Models.Models;
using MovieLibrary.Models.Requests.SubscriptionRequests;

namespace MovieLibrary.Models.Mediatr.SubscriptionCommands
{
    public record UpdateSubscriptionCommand(UpdateSubscriptionRequest subscription) : IRequest<Subscription>
    {
    }
}
