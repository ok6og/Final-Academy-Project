using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MovieLibrary.DL.Interfaces;
using MovieLibrary.Models.Mediatr.UserCommands;

namespace MovieLibrary.BL.CommandHandlers.UserCommandHandlers
{
    public class DoesUserHaveSubscriptionCommandHandler : IRequestHandler<DoesUserHaveSubscriptionCommand, bool>
    {
        private readonly IUserRepository _userRepository;

        public DoesUserHaveSubscriptionCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(DoesUserHaveSubscriptionCommand request, CancellationToken cancellationToken)
        {
            return await _userRepository.DoesUserHaveSubscription(request.userId);
        }
    }
}
