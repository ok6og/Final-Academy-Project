using Kafka.HostedService;
using Kafka.ProducerConsumer.Generic;
using MovieLibrary.BL.Services;
using MovieLibrary.DL.Interfaces;
using MovieLibrary.DL.Repository.MongoDbRepository;
using MovieLibrary.DL.Repository.MsSqlRepository;
using MovieLibrary.Kafka.DataFlow;
using MovieLibrary.Models.Models;

namespace Movie_Library_Final_Project.Extensions
{
    public static class RepoServiceExtensions
    {
        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {

            services.AddSingleton<IMovieRepository, MovieRepository>();
            services.AddSingleton<ISubscriptionRepository, SubscriptionRepository>();
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<IPlanRepository, PlanRepository>();
            services.AddSingleton<IMonthlyProfitRepository, MonthlyProfitRepository>();
            services.AddSingleton<IWatchedMoviesRepository, WatchedMoviesRepository>();
            services.AddSingleton<IWatchListRepository, WatchListRepository>();
            return services;
        }
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddHostedService<HostedServiceSubscriptionConsumer>();
            services.AddSingleton<IDataFlowMonthlyProfitService, DataFlowServiceSubscription>();
            services.AddSingleton<IDataFlowEnrichUsersService, DataFlowEnrichUsers>();
            services.AddSingleton<KafkaProducer<int, Subscription>>();
            return services;
        }
    }
}
