using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MovieLibrary.Models.Models;
using MovieLibrary.Models.Requests.UserRequests;

namespace MovieLibrary.Models.Mediatr.UserCommands
{
    public record UpdateUserCommand(UpdateUserRequest user) : IRequest<User>
    {
    }
}
