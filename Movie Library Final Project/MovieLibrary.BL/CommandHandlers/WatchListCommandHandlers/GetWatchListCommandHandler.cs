using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MovieLibrary.DL.Interfaces;
using MovieLibrary.Models.Mediatr.WatchListCommands;
using MovieLibrary.Models.Models;
using MovieLibrary.Models.MongoDbModels;
using MovieLibrary.Models.Responses;

namespace MovieLibrary.BL.CommandHandlers.WatchListCommandHandlers
{
    public class GetWatchListCommandHandler : IRequestHandler<GetWatchListCommand, HttpResponse<IEnumerable<Watchlist>>>
    {
        private readonly IWatchListRepository _watchListRepository;

        public GetWatchListCommandHandler(IWatchListRepository watchListRepository)
        {
            _watchListRepository = watchListRepository;
        }

        public async Task<HttpResponse<IEnumerable<Watchlist>>> Handle(GetWatchListCommand request, CancellationToken cancellationToken)
        {
            if (request.userId <= 0)
            {
                return new HttpResponse<IEnumerable<Watchlist>>
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = StaticResponses.UserIdLessThanOrEqualTo0,
                    Value = null
                };
            }
            var listOfwatchList = await _watchListRepository.GetContent(request.userId);
            if (listOfwatchList.Count() == 0)
            {
                return new HttpResponse<IEnumerable<Watchlist>>
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "There is no watchlist for this user",
                    Value = null
                };
            }
            if (!listOfwatchList.FirstOrDefault().WatchList.ToList().Any())
            {
                return new HttpResponse<IEnumerable<Watchlist>> 
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Message = "There are no movies to be watched in the watchlist",
                    Value = null
                };
            }
            return new HttpResponse<IEnumerable<Watchlist>>
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Returned list of movies to watch",
                Value = listOfwatchList
            };
        }
    }
}
