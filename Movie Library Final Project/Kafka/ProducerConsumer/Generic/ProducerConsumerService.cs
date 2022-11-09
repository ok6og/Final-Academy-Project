using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieLibrary.Models.Models;

namespace Kafka.ProducerConsumer.Generic
{
    public abstract class ProducerConsumerService : IKafkaProducer<int, Subscription>, IKafkaConsumer<Subscription>
    {
        public Task ConsumeValues(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public abstract Task HandleMesseges(Subscription value);


        public void Produce(int key, Subscription value)
        {
            throw new NotImplementedException();
        }
    }
}
