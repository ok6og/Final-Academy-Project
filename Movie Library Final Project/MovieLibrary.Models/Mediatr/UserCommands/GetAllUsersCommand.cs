using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MovieLibrary.Models.Models;

namespace MovieLibrary.Models.Mediatr.UserCommands
{
    public record GetAllUsersCommand : IRequest<IEnumerable<User>>
    {
    }
}
