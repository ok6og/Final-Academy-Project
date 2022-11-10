namespace Kafka.ProducerConsumer.Generic
{
    public interface IKafkaProducer<TKey, TValue>
    {
        void Produce(TKey key, TValue value);
    }
}