using MovieLibrary.Models.Models;

namespace MovieLibrary.Kafka.DataFlow
{
    public interface IDataFlowEnrichUsersService
    {
        public void HandleSubscriptions(Subscription subscription);
    }
}
