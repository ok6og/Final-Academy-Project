using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using AutoMapper;
using Kafka.KafkaConfig;
using Kafka.ProducerConsumer.Generic;
using Microsoft.Extensions.Options;
using MovieLibrary.DL.Interfaces;
using MovieLibrary.Models.Models;
using MovieLibrary.Models.Responses;

namespace Kafka.ProducerConsumer
{
    public class SubscriptionConsumer : KafkaConsumer<int, Subscription>
    {
        private readonly TransformBlock<Subscription, SubscriptionResponse> _transformBlock;
        private readonly ActionBlock<SubscriptionResponse> _actionBlock;
        private readonly IUserRepository _userRepo;
        private readonly IPlanRepository _planRepo;
        private readonly IMapper _mapper;
        public SubscriptionConsumer(IOptionsMonitor<List<MyKafkaSettings>> kafkaSettings, IUserRepository userRepo, IPlanRepository planRepo) : base(kafkaSettings)
        {
            _userRepo = userRepo;
            _planRepo = planRepo;
            _transformBlock = new TransformBlock<Subscription, SubscriptionResponse>(async sub =>
            {
                var subResponse = _mapper.Map<SubscriptionResponse>(sub);
                subResponse.Plan = await _planRepo.GetPlanById(sub.PlanId);
                subResponse.User = await _userRepo.GetUserById(sub.UserId);
                return subResponse;
            });
            _actionBlock = new ActionBlock<SubscriptionResponse>(value =>
            {
                value.Plan.PricePerMonth++;
            });
            _transformBlock.LinkTo(_actionBlock);
        }

        public override Task HandleMesseges(Subscription value)
        {
            _transformBlock.Post(value);
            return Task.CompletedTask;
        }
    }
}
