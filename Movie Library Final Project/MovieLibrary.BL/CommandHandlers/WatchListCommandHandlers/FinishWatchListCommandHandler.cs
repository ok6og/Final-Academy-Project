using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Confluent.Kafka;
using MediatR;
using MovieLibrary.DL.Interfaces;
using MovieLibrary.Models.Mediatr.WatchListCommands;
using MovieLibrary.Models.Models;
using MovieLibrary.Models.MongoDbModels;
using MovieLibrary.Models.Responses;

namespace MovieLibrary.BL.CommandHandlers.WatchListCommandHandlers
{
    public class FinishWatchListCommandHandler : IRequestHandler<FinishWatchListCommand, HttpResponse<WatchedList>>
    {
        private readonly IWatchListRepository _watchListRepository;
        private readonly IWatchedMoviesRepository _watchedMoviesListRepository;

        public FinishWatchListCommandHandler(IWatchedMoviesRepository watchedMoviesListRepository, IWatchListRepository watchListRepository)
        {
            _watchedMoviesListRepository = watchedMoviesListRepository;
            _watchListRepository = watchListRepository;
        }

        public async Task<HttpResponse<WatchedList>> Handle(FinishWatchListCommand request, CancellationToken cancellationToken)
        {
            int userId = request.userId;
            if (userId <= 0)
            {
                return new HttpResponse<WatchedList>
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = StaticResponses.UserIdLessThanOrEqualTo0,
                    Value = null
                };
            }
            var finishedWatchList = await _watchListRepository.GetWatchList(userId);
            if (finishedWatchList == null)
            {
                return new HttpResponse<WatchedList>
                {
                    StatusCode = HttpStatusCode.NoContent,
                    Message = "You cant finish an empty watch list",
                    Value = null
                };
            }
            var listMovies = await _watchedMoviesListRepository.GetWatchedMovies(userId);
            var thisMovies = listMovies.FirstOrDefault();
            if (thisMovies == null)
            {
                var watchedList = new WatchedList()
                {
                    Id = Guid.NewGuid(),
                    WatchedMovies = finishedWatchList.WatchList,
                    TotalTimeSpendInMovies = finishedWatchList.WatchList.Sum(x => x.LengthInMinutes),
                    UserId = userId
                };
                await _watchListRepository.RemoveWatchList(userId);
                await _watchedMoviesListRepository.SaveWatchedMovies(watchedList);
                return new HttpResponse<WatchedList>
                {
                    StatusCode = HttpStatusCode.OK,
                    Message = "Added a new list of watched movies",
                    Value = watchedList
                };
            }
            var allMovies = finishedWatchList.WatchList.ToList();
            allMovies.AddRange(thisMovies.WatchedMovies);
            var watchedListAll = new WatchedList()
            {
                Id = thisMovies.Id,
                UserId = userId,
                WatchedMovies = allMovies,
                TotalTimeSpendInMovies = allMovies.Sum(x => x.LengthInMinutes)
            };
            await _watchListRepository.RemoveWatchList(userId);
            await _watchedMoviesListRepository.UpdateWatchedMovies(watchedListAll);
            return new HttpResponse<WatchedList>
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Added a to the list of watched movies",
                Value = watchedListAll
            };
        }
    }
}
