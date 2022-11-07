using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieLibrary.Models.Models;

namespace MovieLibrary.DL.Interfaces
{
    public interface ISubscriptionRepository
    {
        Task<IEnumerable<Subscription?>> GetAllSubscriptions();
        Task<Subscription?> GetSubscriptionById(int movieId);
        Task<Subscription?> AddSubscription(Subscription movie, int months);
        Task<Subscription?> UpdatSubscription(Subscription movie);
        Task<Subscription?> DeleteSubscription(int movieId);
        Task<IEnumerable<Subscription?>> GetAllSubscriptionsForMonth();

    }
}
