﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MovieLibrary.Models.MongoDbModels;
using MovieLibrary.Models.Responses;

namespace MovieLibrary.Models.Mediatr.WatchListCommands
{
    public record GetWatchListCommand(int userId) : IRequest<HttpResponse<IEnumerable<Watchlist>>>
    {
    }
}
