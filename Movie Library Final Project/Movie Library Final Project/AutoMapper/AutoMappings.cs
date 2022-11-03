using AutoMapper;
using MovieLibrary.Models.Models;
using MovieLibrary.Models.Requests.MovieRequests;
using MovieLibrary.Models.Requests.PlanRequests;
using MovieLibrary.Models.Requests.SubscriptionRequests;
using MovieLibrary.Models.Requests.UserRequests;

namespace Movie_Library_Final_Project.AutoMapper
{
    internal class AutoMappings : Profile
    {
        public AutoMappings()
        {
            CreateMap<AddMovieRequest, Movie>();
            CreateMap<UpdateMovieRequest, Movie>();
            CreateMap<AddSubscriptionRequest, Subscription>();
            CreateMap<UpdateSubscriptionRequest, Subscription>();
            CreateMap<AddUserRequest, User>();
            CreateMap<UpdateUserRequest, User>();
            CreateMap<AddPlanRequest,Plan>();
            CreateMap<UpdatePlanRequest, Plan>();
        }
    }
}
