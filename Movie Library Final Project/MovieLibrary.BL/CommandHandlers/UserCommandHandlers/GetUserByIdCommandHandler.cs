using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MovieLibrary.DL.Interfaces;
using MovieLibrary.Models.Mediatr.UserCommands;
using MovieLibrary.Models.Models;

namespace MovieLibrary.BL.CommandHandlers.UserCommandHandlers
{
    public class GetUserByIdCommandHandler : IRequestHandler<GetUserByIdCommand,User>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByIdCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> Handle(GetUserByIdCommand request, CancellationToken cancellationToken)
        {
            return await _userRepository.GetUserById(request.userId);
        }
    }
}
