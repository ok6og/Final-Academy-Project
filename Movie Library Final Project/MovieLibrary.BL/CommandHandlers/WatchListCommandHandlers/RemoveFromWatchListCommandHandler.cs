﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MovieLibrary.DL.Interfaces;
using MovieLibrary.Models.Mediatr.WatchListCommands;
using MovieLibrary.Models.Models;
using MovieLibrary.Models.Responses;

namespace MovieLibrary.BL.CommandHandlers.WatchListCommandHandlers
{
    public class RemoveFromWatchListCommandHandler : IRequestHandler<RemoveMovieFromWatchListCommand, HttpResponse<Movie>>
    {
        public readonly IWatchListRepository _watchListRepository;

        public RemoveFromWatchListCommandHandler(IWatchListRepository watchListRepository)
        {
            _watchListRepository = watchListRepository;
        }

        public async Task<HttpResponse<Movie>> Handle(RemoveMovieFromWatchListCommand request, CancellationToken cancellationToken)
        {
            var watchlist = await _watchListRepository.GetWatchList(request.userId);
            if (watchlist == null)
            {
                return new HttpResponse<Movie>
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Message = "There is no watchlist to remove movies from",
                    Value = null
                };
            }
            var movie = await _watchListRepository.RemoveFromWatchList(request.userId, request.movieId);
            if (movie == null)
            {
                return new HttpResponse<Movie>
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Message = "There is no such movie to be removed from the watch list",
                    Value = null
                };
            }
            return new HttpResponse<Movie>
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Successfully removed a movie from watchlist",
                Value = movie
            };
        }
    }
}
