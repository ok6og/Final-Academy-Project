using AutoMapper;
using MovieLibrary.Models.Models;
using MovieLibrary.Models.Requests.MovieRequests;
using MovieLibrary.Models.Requests.PlanRequests;
using MovieLibrary.Models.Requests.SubscriptionRequests;
using MovieLibrary.Models.Requests.UserRequests;
using MovieLibrary.Models.Responses;

namespace Movie_Library_Final_Project.AutoMapper
{
    public class AutoMappings : Profile
    {
        public AutoMappings()
        {
            CreateMap<AddMovieRequest, Movie>();
            CreateMap<UpdateMovieRequest, Movie>();
            CreateMap<AddSubscriptionRequest, Subscription>();
            CreateMap<UpdateSubscriptionRequest, Subscription>();
            CreateMap<AddSubscriptionRequest, SubscriptionResponse>();
            CreateMap<UpdateSubscriptionRequest, SubscriptionResponse>();
            CreateMap<Subscription, SubscriptionResponse>();
            CreateMap<AddUserRequest, User>();
            CreateMap<UpdateUserRequest, User>();
            CreateMap<AddPlanRequest, Plan>();
            CreateMap<UpdatePlanRequest, Plan>();
        }
    }
}
