using AutoMapper;
using MediatR;
using MovieLibrary.DL.Interfaces;
using MovieLibrary.Models.Mediatr.SubscriptionCommands;
using MovieLibrary.Models.Models;
using MovieLibrary.Models.Responses;

namespace MovieLibrary.BL.CommandHandlers.SubscriptionCommandHandlers
{
    public class UpdateSubscriptionCommandHandler : IRequestHandler<UpdateSubscriptionCommand, HttpResponse<Subscription>>
    {
        private ISubscriptionRepository _subsRepo;
        private IMapper _mapper;

        public UpdateSubscriptionCommandHandler(ISubscriptionRepository subsrepo, IMapper mapper)
        {
            _subsRepo = subsrepo;
            _mapper = mapper;
        }
        public async Task<HttpResponse<Subscription>> Handle(UpdateSubscriptionCommand request, CancellationToken cancellationToken)
        {
            var sub = _mapper.Map<Subscription>(request.subscription);
            var result = await _subsRepo.UpdatSubscription(sub);
            var response = new HttpResponse<Subscription>()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Message = "Successfully updated a subscrption",
                Value = result
            };
            if (result == null)
            {
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                response.Message = "There is not subscription with such Id";
            }
            return response;
        }
    }
}
