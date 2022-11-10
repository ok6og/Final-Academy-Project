using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MovieLibrary.DL.Interfaces;
using MovieLibrary.Models.Mediatr.MovieCommands;
using MovieLibrary.Models.Mediatr.UserCommands;
using MovieLibrary.Models.Models;
using MovieLibrary.Models.Responses;

namespace MovieLibrary.BL.CommandHandlers.UserCommandHandlers
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, HttpResponse<User>>
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<HttpResponse<User>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            if (await _userRepository.DoesUserHaveSubscription(request.userId))
            {
                return new HttpResponse<User>()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "An user with a subscription cannot be deleted",
                    Value = null
                };
            }
            var userExist = await _userRepository.DeleteUser(request.userId);
            if (userExist == null)
            {
                return new HttpResponse<User>()
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Message = "User does not exist",
                    Value = null
                };
            }
            return new HttpResponse<User>()
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Successfully deleted an user",
                Value = userExist
            };
        }
    }
}
