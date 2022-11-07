using Kafka.HostedService;
using Kafka.ProducerConsumer.Generic;
using MovieLibrary.DL.Interfaces;
using MovieLibrary.DL.Repository;
using MovieLibrary.Models.Models;
using MovieLibrary.Models.Responses;

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
            services.AddSingleton<KafkaProducer<int, Subscription>>();
            services.AddHostedService<HostedServiceSubscriptionConsumer>();

            return services;
        }
    }
}
