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
using MovieLibrary.Models.Responses;

namespace MovieLibrary.BL.CommandHandlers.UserCommandHandlers
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand,HttpResponse<User>>
    {
        private IUserRepository _userRepo;
        private IMapper _mapper;

        public UpdateUserCommandHandler(IUserRepository userRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            _mapper = mapper;
        }

        public async Task<HttpResponse<User>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(request.user);
            var result = await _userRepo.UpdateUser(user);
            var response = new HttpResponse<User>()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Message = "Successfully added an user",
                Value = result
            };
            if (result == null)
            {
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                response.Message = "There is not user with such Id";
            }
            return response;
        }
    }
}
