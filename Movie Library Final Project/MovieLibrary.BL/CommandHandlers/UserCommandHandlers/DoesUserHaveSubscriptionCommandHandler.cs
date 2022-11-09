using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MovieLibrary.DL.Interfaces;
using MovieLibrary.Models.Mediatr.UserCommands;
using MovieLibrary.Models.Responses;

namespace MovieLibrary.BL.CommandHandlers.UserCommandHandlers
{
    public class DoesUserHaveSubscriptionCommandHandler : IRequestHandler<DoesUserHaveSubscriptionCommand, HttpResponse<bool>>
    {
        private readonly IUserRepository _userRepository;

        public DoesUserHaveSubscriptionCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<HttpResponse<bool>> Handle(DoesUserHaveSubscriptionCommand request, CancellationToken cancellationToken)
        {
            var isSubscribed = await _userRepository.DoesUserHaveSubscription(request.userId);
            var response = new HttpResponse<bool>()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Message = "The user has a subscription",
                Value = isSubscribed
            };
            if (isSubscribed == false)
            {
                response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                response.Message = "This user does not have subscription";  
            }
            return response;
        }
    }
}
