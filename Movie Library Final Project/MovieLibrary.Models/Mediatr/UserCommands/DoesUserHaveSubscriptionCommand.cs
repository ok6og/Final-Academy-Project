using MediatR;
using MovieLibrary.Models.Responses;

namespace MovieLibrary.Models.Mediatr.UserCommands
{
    public record DoesUserHaveSubscriptionCommand(int userId) : IRequest<HttpResponse<bool>>
    {
    }
}
