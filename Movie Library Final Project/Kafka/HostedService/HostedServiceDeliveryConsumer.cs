using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Kafka.KafkaConfig;
using Kafka.ProducerConsumer;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MovieLibrary.DL.Interfaces;

namespace Kafka.HostedService
{
    public class HostedServiceSubscriptionConsumer : IHostedService
    {
        private readonly SubscriptionConsumer _subsConsumer;
        private readonly IUserRepository _userRepository;
        private readonly IPlanRepository _planRepository;
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IMonthlyProfitRepository _monthlyProfitRepository;
        private readonly IOptionsMonitor<List<MyKafkaSettings>> _kafkaSettings;
        private readonly IMapper _mapper;


        public HostedServiceSubscriptionConsumer(IOptionsMonitor<List<MyKafkaSettings>> kafkaSettings, IPlanRepository planRepository, IUserRepository userRepository, IMapper mapper, ISubscriptionRepository subscriptionRepository, IMonthlyProfitRepository monthlyProfitRepository)
        {
            _mapper = mapper;
            _kafkaSettings = kafkaSettings;
            _planRepository = planRepository;
            _userRepository = userRepository;
            _subscriptionRepository = subscriptionRepository;
            _monthlyProfitRepository = monthlyProfitRepository;
            _subsConsumer = new SubscriptionConsumer(_kafkaSettings, _userRepository, _planRepository, _mapper, _subscriptionRepository, _monthlyProfitRepository);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _subsConsumer.ConsumeValues(cancellationToken);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
