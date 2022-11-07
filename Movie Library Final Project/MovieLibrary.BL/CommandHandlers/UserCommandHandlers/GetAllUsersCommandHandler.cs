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
    public class GetAllUsersCommandHandler : IRequestHandler<GetAllUsersCommand, IEnumerable<User>>
    {
        private readonly IUserRepository _userRepository;

        public GetAllUsersCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<User>> Handle(GetAllUsersCommand request, CancellationToken cancellationToken)
        {
            return await _userRepository.GetAllUsers();
        }
    }
}
