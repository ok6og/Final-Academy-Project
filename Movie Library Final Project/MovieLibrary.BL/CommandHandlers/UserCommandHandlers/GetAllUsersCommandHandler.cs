using MediatR;
using MovieLibrary.DL.Interfaces;
using MovieLibrary.Models.Mediatr.UserCommands;
using MovieLibrary.Models.Models;
using MovieLibrary.Models.Responses;

namespace MovieLibrary.BL.CommandHandlers.UserCommandHandlers
{
    public class GetAllUsersCommandHandler : IRequestHandler<GetAllUsersCommand, HttpResponse<IEnumerable<User>>>
    {
        private readonly IUserRepository _userRepository;

        public GetAllUsersCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<HttpResponse<IEnumerable<User>>> Handle(GetAllUsersCommand request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllUsers();
            var response = new HttpResponse<IEnumerable<User>>()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Message = "Successfully retrieved all users",
                Value = users
            };
            if (users == null)
            {
                response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                response.Message = "There are no users in the database";
            }
            return response;
        }
    }
}
