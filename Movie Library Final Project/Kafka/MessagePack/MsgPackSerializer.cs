using Confluent.Kafka;
using MessagePack;

namespace Kafka.MessagePack
{
    public class MsgPackSerializer<TValue> : ISerializer<TValue>
    {
        public byte[] Serialize(TValue data, SerializationContext context)
        {
            return MessagePackSerializer.Serialize(data);
        }
    }
}
