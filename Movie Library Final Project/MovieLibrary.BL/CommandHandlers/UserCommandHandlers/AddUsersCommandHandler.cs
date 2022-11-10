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
    public class AddUsersCommandHandler : IRequestHandler<AddUserCommand, HttpResponse<User>>
    {
        private IUserRepository _userRepo;
        private IMapper _mapper;

        public AddUsersCommandHandler(IUserRepository userRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            _mapper = mapper;
        }
        public async Task<HttpResponse<User>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(request.user);
            var result = await _userRepo.AddUser(user);
            var response = new HttpResponse<User>()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Message = "Successfully added an user",
                Value = result
            };
            if (result == null)
            {
                response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                response.Message = "User could'not be added";
                return response;
            }
            if (result.UserId <= 0)
            {
                response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                response.Message = "Invalid Id";
            }
            return response;
        }
    }
}
