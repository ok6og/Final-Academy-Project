using MovieLibrary.DL.Interfaces;
using MovieLibrary.DL.Repository;

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

            return services;
        }
    }
}
