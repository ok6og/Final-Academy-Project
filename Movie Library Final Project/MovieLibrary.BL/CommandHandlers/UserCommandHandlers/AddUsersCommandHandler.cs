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
            if (result == null)
            {
                return new HttpResponse<User>()
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Message = "User could'not be added",
                    Value = null
                };
            }
            if (result.UserId <= 0)
            {
                return new HttpResponse<User>()
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Message = StaticResponses.UserIdLessThanOrEqualTo0,
                    Value = null
                };
            }
            return new HttpResponse<User>()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Message = "Successfully added an user",
                Value = result
            };
        }
    }
}
