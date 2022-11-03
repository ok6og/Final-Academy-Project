using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MovieLibrary.DL.Interfaces;
using MovieLibrary.Models.Mediatr.UserCommands;
using MovieLibrary.Models.Models;

namespace MovieLibrary.BL.CommandHandlers.UserCommandHandlers
{
    public class AddUsersCommandHandler : IRequestHandler<AddUserCommand, User>
    {
        private IUserRepository _userRepo;
        private IMapper _mapper;

        public AddUsersCommandHandler(IUserRepository userRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            _mapper = mapper;
        }
        public async Task<User> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(request.user);
            return await _userRepo.AddUser(user);
        }
    }
}
