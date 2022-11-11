using Kafka.KafkaConfig;
using Kafka.ProducerConsumer.Generic;
using Microsoft.Extensions.Options;
using MovieLibrary.Kafka.DataFlow;
using MovieLibrary.Models.Models;

namespace Kafka.ProducerConsumer
{
    public class SubscriptionConsumer : KafkaConsumer<int, Subscription>
    {
        private readonly IDataFlowMonthlyProfitService _dataFlowService;
        private readonly IDataFlowEnrichUsersService _dataFlowEnrichUsersService;

        public SubscriptionConsumer(IOptionsMonitor<List<MyKafkaSettings>> kafkaSettings, IDataFlowMonthlyProfitService dataFlowService, IDataFlowEnrichUsersService dataFlowEnrichUsersService) : base(kafkaSettings)
        {
            _dataFlowService = dataFlowService;
            _dataFlowEnrichUsersService = dataFlowEnrichUsersService;
        }
        public override Task ConsumeValues(CancellationToken cancellationToken)
        {
            Task.Run(() =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    try
                    {
                        var value = _consumer.Consume();
                        var objectValue = value.Value;
                        HandleMesseges(objectValue);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
            }, cancellationToken);
            return Task.CompletedTask;
        }
        public override Task HandleMesseges(Subscription value)
        {
            _dataFlowService.HandleSubscriptions(value);
            _dataFlowEnrichUsersService.HandleSubscriptions(value);
            return Task.CompletedTask;
        }
    }
}
