using MediatR;
using MovieLibrary.Models.Models;
using MovieLibrary.Models.Responses;

namespace MovieLibrary.Models.Mediatr.UserCommands
{
    public record GetAllUsersCommand : IRequest<HttpResponse<IEnumerable<User>>>
    {
    }
}
