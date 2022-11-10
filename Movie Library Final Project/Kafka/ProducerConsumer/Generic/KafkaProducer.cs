using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Confluent.Kafka;
using Kafka.KafkaConfig;
using Kafka.MessagePack;
using Microsoft.Extensions.Options;

namespace Kafka.ProducerConsumer.Generic
{
    public class KafkaProducer<TKey, TValue> : IKafkaProducer<TKey, TValue>
    {
        private readonly IOptionsMonitor<List<MyKafkaSettings>> _kafkaSettings;
        private readonly IProducer<TKey, TValue> _producer;
        private readonly MyKafkaSettings _thisKafkaSettings;
        private readonly ProducerConfig _config;

        public KafkaProducer(IOptionsMonitor<List<MyKafkaSettings>> kafkaSettings)
        {
            _kafkaSettings = kafkaSettings;
            _thisKafkaSettings = _kafkaSettings.CurrentValue.FirstOrDefault(x => x.objectType.Contains(typeof(TValue).Name));
            _config = new ProducerConfig()
            {
                BootstrapServers = _thisKafkaSettings.BootstrapServers
            };
            _producer = new ProducerBuilder<TKey, TValue>(_config)
                .SetValueSerializer(new MsgPackSerializer<TValue>())
                .SetKeySerializer(new MsgPackSerializer<TKey>()).Build();
        }
        public async void Produce(TKey key, TValue value)
        {
            try
            {
                var msg = new Message<TKey, TValue>()
                {
                    Key = key,
                    Value = value
                };
                var result = await _producer.ProduceAsync(_thisKafkaSettings.Topic, msg);
                if (result != null)
                    Console.WriteLine($"Delivered: {result.Value} to {result.TopicPartitionOffset}");
            }
            catch (ProduceException<TKey, TValue> ex)
            {
                Console.WriteLine($"Delivery failed: {ex.Error.Reason}"); ;
            }
        }
    }
}
