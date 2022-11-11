using Confluent.Kafka;
using Kafka.KafkaConfig;
using Kafka.MessagePack;
using Microsoft.Extensions.Options;

namespace Kafka.ProducerConsumer.Generic
{
    public abstract class KafkaConsumer<TKey, TValue> : IKafkaConsumer<TValue>
    {
        public readonly IOptionsMonitor<List<MyKafkaSettings>> _kafkaSettings;
        private readonly MyKafkaSettings _thisKafkaSettings;
        private readonly ConsumerConfig _config;
        protected readonly IConsumer<TKey, TValue> _consumer;

        public KafkaConsumer(IOptionsMonitor<List<MyKafkaSettings>> kafkaSettings)
        {
            _kafkaSettings = kafkaSettings;
            _thisKafkaSettings = kafkaSettings.CurrentValue.First(x => x.objectType.Contains(typeof(TValue).Name));
            _config = new ConsumerConfig()
            {
                BootstrapServers = _thisKafkaSettings.BootstrapServers,
                AutoOffsetReset = AutoOffsetReset.Earliest,
                GroupId = _thisKafkaSettings.GroupId

            };
            _consumer = new ConsumerBuilder<TKey, TValue>(_config)
                .SetValueDeserializer(new MsgPackDeserializer<TValue>())
                .SetKeyDeserializer(new MsgPackDeserializer<TKey>()).Build();
            _consumer.Subscribe(_thisKafkaSettings.Topic);
        }
        public abstract Task ConsumeValues(CancellationToken cancellationToken);

        public abstract Task HandleMesseges(TValue value);
    }
}
