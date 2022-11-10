namespace Kafka.ProducerConsumer.Generic
{
    public interface IKafkaConsumer<TValue>
    {
        Task ConsumeValues(CancellationToken cancellationToken);
        Task HandleMesseges(TValue value);
    }
}