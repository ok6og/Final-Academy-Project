using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MovieLibrary.DL.Interfaces;
using MovieLibrary.Models.Mediatr.UserCommands;
using MovieLibrary.Models.Models;
using MovieLibrary.Models.Responses;

namespace MovieLibrary.BL.CommandHandlers.UserCommandHandlers
{
    public class GetUserByIdCommandHandler : IRequestHandler<GetUserByIdCommand,HttpResponse<User>>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByIdCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<HttpResponse<User>> Handle(GetUserByIdCommand request, CancellationToken cancellationToken)
        {
            var result = await _userRepository.GetUserById(request.userId);
            var response = new HttpResponse<User>()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Message = "Successfully gotten an user",
                Value = result
            };
            if (result == null)
            {
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                response.Message = "User not found";
            }
            return response;
        }
    }
}
