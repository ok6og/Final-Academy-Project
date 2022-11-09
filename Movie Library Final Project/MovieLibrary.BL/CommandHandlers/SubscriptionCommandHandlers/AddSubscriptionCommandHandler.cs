using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Kafka.KafkaConfig;
using Kafka.ProducerConsumer.Generic;
using MediatR;
using Microsoft.Extensions.Options;
using MovieLibrary.DL.Interfaces;
using MovieLibrary.Models.Mediatr.SubscriptionCommands;
using MovieLibrary.Models.Models;
using MovieLibrary.Models.Responses;

namespace MovieLibrary.BL.CommandHandlers.SubscriptionCommandHandlers
{
    public class AddSubscriptionCommandHandler : IRequestHandler<AddSubscriptionCommand, HttpResponse<SubscriptionResponse>>
    {
        private readonly ISubscriptionRepository _subsRepo;
        private readonly IPlanRepository _planRepo;
        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;
        private readonly KafkaProducer<int, Subscription> _kafkaProducer;
        private readonly IOptionsMonitor<List<MyKafkaSettings>> _kafkaSettings;



        public AddSubscriptionCommandHandler(ISubscriptionRepository subsrepo, IMapper mapper, IPlanRepository planRepo, IUserRepository userRepo, IOptionsMonitor<List<MyKafkaSettings>> kafkaSettings)
        {
            _kafkaSettings = kafkaSettings;
            _subsRepo = subsrepo;
            _mapper = mapper;
            _planRepo = planRepo;
            _userRepo = userRepo;
            _kafkaProducer = new KafkaProducer<int, Subscription>(_kafkaSettings);
        }
        public async Task<HttpResponse<SubscriptionResponse>> Handle(AddSubscriptionCommand request, CancellationToken cancellationToken)
        {
            var sub = _mapper.Map<Subscription>(request.subscription);
            var subWithId = await _subsRepo.AddSubscription(sub, request.months);
            var subResponse = _mapper.Map<SubscriptionResponse>(subWithId);
            subResponse.Plan =await _planRepo.GetPlanById(sub.PlanId);
            subResponse.User = await _userRepo.GetUserById(sub.UserId);

            var response = new HttpResponse<SubscriptionResponse>()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Message = "Successfully added a subscription",
                Value = subResponse
            };
            if (subWithId == null)
            {
                response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                response.Message = "Subscription could not be added";
                return response;
            }
            _kafkaProducer.Produce(subWithId.SubscriptionId, subWithId);
            return response;
        }
    }
}
