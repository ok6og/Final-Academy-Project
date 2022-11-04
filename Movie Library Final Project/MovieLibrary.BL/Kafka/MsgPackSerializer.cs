using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Confluent.Kafka;
using MessagePack;

namespace MovieLibrary.BL.Kafka
{
    public class MsgPackSerializer<TValue> : ISerializer<TValue>
    {
        public byte[] Serialize(TValue data, SerializationContext context)
        {
            return MessagePackSerializer.Serialize(data);
        }
    }
}
