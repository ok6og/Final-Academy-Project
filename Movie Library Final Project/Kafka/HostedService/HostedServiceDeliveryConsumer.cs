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
using MovieLibrary.Kafka.DataFlow;

namespace Kafka.HostedService
{
    public class HostedServiceSubscriptionConsumer : IHostedService
    {
        private readonly SubscriptionConsumer _subsConsumer;
        private readonly IDataFlowServiceSubscriptions _dataFlowServiceSubscriptions;
        private readonly IOptionsMonitor<List<MyKafkaSettings>> _kafkaSettings;


        public HostedServiceSubscriptionConsumer(IOptionsMonitor<List<MyKafkaSettings>> kafkaSettings,  IDataFlowServiceSubscriptions dataFlowServiceSubscriptions)
        {
            _kafkaSettings = kafkaSettings;
            _dataFlowServiceSubscriptions = dataFlowServiceSubscriptions;
            _subsConsumer = new SubscriptionConsumer(_kafkaSettings, _dataFlowServiceSubscriptions);
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
