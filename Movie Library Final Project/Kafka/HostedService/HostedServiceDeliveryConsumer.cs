using Kafka.KafkaConfig;
using Kafka.ProducerConsumer;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MovieLibrary.Kafka.DataFlow;

namespace Kafka.HostedService
{
    public class HostedServiceSubscriptionConsumer : IHostedService
    {
        private readonly SubscriptionConsumer _subsConsumer;
        private readonly IDataFlowMonthlyProfitService _dataFlowServiceSubscriptions;
        private readonly IDataFlowEnrichUsersService _dataFlowEnrichUsersServiceSubscriptions;
        private readonly IOptionsMonitor<List<MyKafkaSettings>> _kafkaSettings;


        public HostedServiceSubscriptionConsumer(IOptionsMonitor<List<MyKafkaSettings>> kafkaSettings, IDataFlowMonthlyProfitService dataFlowServiceSubscriptions, IDataFlowEnrichUsersService dataFlowEnrichUsersServiceSubscriptions)
        {
            _kafkaSettings = kafkaSettings;
            _dataFlowServiceSubscriptions = dataFlowServiceSubscriptions;
            _dataFlowEnrichUsersServiceSubscriptions = dataFlowEnrichUsersServiceSubscriptions;
            _subsConsumer = new SubscriptionConsumer(_kafkaSettings, _dataFlowServiceSubscriptions, _dataFlowEnrichUsersServiceSubscriptions);
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
