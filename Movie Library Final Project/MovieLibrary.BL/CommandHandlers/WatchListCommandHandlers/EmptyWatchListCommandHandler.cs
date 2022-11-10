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
    public class EmptyWatchListCommandHandler : IRequestHandler<EmptyWatchListCommand, HttpResponse<Watchlist>>
    {
        private readonly IWatchListRepository _watchListRepository;

        public EmptyWatchListCommandHandler(IWatchListRepository watchListRepository)
        {
            _watchListRepository = watchListRepository;
        }

        public async Task<HttpResponse<Watchlist>> Handle(EmptyWatchListCommand request, CancellationToken cancellationToken)
        {
            if (request.userId <= 0)
            {
                return new HttpResponse<Watchlist>
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = StaticResponses.UserIdLessThanOrEqualTo0,
                    Value = null
                };
            }
            var watchlist = await  _watchListRepository.GetWatchList(request.userId);
            if (watchlist == null)
            {
                return new HttpResponse<Watchlist>
                {
                    Message = StaticResponses.WatchListDoesNotExist,
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Value = null
                };
            }
            await _watchListRepository.EmptyWatchList(request.userId);
            return new HttpResponse<Watchlist>
            {
                Message = StaticResponses.SuccessfullyCompletedTheOperation,
                StatusCode = System.Net.HttpStatusCode.OK,
                Value = watchlist
            };
        }
    }
}
