using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MovieLibrary.DL.Interfaces;
using MovieLibrary.Models.Mediatr.WatchedMoviesCommands;
using MovieLibrary.Models.Models;
using MovieLibrary.Models.MongoDbModels;
using MovieLibrary.Models.Responses;

namespace MovieLibrary.BL.CommandHandlers.WatchedMoviesListCommandHandler
{
    public class GetWatchedMoviesListCommandHandler : IRequestHandler<GetWatchedMoviesListCommand, HttpResponse<IEnumerable<WatchedList>>>
    {
        private readonly IWatchedMoviesRepository _watchedMoviesRepository;

        public GetWatchedMoviesListCommandHandler(IWatchedMoviesRepository watchedMoviesRepository)
        {
            _watchedMoviesRepository = watchedMoviesRepository;
        }

        public async Task<HttpResponse<IEnumerable<WatchedList>>> Handle(GetWatchedMoviesListCommand request, CancellationToken cancellationToken)
        {
            if (request.userId <= 0)
            {
                return new HttpResponse<IEnumerable<WatchedList>>
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Message = StaticResponses.UserIdLessThanOrEqualTo0,
                    Value = null
                };
            }
            var watchedList = await _watchedMoviesRepository.GetWatchedMovies(request.userId);
            var response = new HttpResponse<IEnumerable<WatchedList>>();
            if (watchedList == null)
            {
                return new HttpResponse<IEnumerable<WatchedList>>
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Message = "This user has no watched movies",
                    Value = watchedList
                };
            }
            return new HttpResponse<IEnumerable<WatchedList>>
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Message = "Succesfully retrieved watched movies for user",
                Value = watchedList
            };
        }
    }
}
