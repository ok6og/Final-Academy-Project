using MediatR;
using MovieLibrary.Models.Models;
using MovieLibrary.Models.Responses;

namespace MovieLibrary.Models.Mediatr.UserCommands
{
    public record DeleteUserCommand(int userId) : IRequest<HttpResponse<User>>
    {
    }
}
