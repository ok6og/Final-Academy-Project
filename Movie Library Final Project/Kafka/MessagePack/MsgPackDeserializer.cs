using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Confluent.Kafka;
using MessagePack;

namespace Kafka.MessagePack
{
    public class MsgPackDeserializer<TValue> : IDeserializer<TValue>
    {
        public TValue Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
        {
            return MessagePackSerializer.Deserialize<TValue>(data.ToArray());
        }
    }
}
