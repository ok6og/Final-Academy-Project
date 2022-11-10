using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using AutoMapper;
using Kafka.KafkaConfig;
using Kafka.ProducerConsumer.Generic;
using MessagePack.Formatters;
using Microsoft.Extensions.Options;
using MovieLibrary.DL.Interfaces;
using MovieLibrary.Kafka.DataFlow;
using MovieLibrary.Models.Models;
using MovieLibrary.Models.Responses;

namespace Kafka.ProducerConsumer
{
    public class SubscriptionConsumer : KafkaConsumer<int, Subscription>
    {
        private readonly IDataFlowServiceSubscriptions _dataFlowService;

        public SubscriptionConsumer(IOptionsMonitor<List<MyKafkaSettings>> kafkaSettings, IDataFlowServiceSubscriptions dataFlowService) : base(kafkaSettings)
        {
            _dataFlowService = dataFlowService;
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
                        Console.WriteLine("THIS IS CONSUMED");
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
            return Task.CompletedTask;
        }
    }
}
