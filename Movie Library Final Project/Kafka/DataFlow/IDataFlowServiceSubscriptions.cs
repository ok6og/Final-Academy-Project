using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieLibrary.Models.Models;

namespace MovieLibrary.Kafka.DataFlow
{
    public interface IDataFlowServiceSubscriptions
    {
        public void HandleSubscriptions(Subscription subscription);
    }
}
