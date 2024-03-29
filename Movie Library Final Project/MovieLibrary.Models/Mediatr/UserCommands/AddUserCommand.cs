﻿using MediatR;
using MovieLibrary.Models.Models;
using MovieLibrary.Models.Requests.UserRequests;
using MovieLibrary.Models.Responses;

namespace MovieLibrary.Models.Mediatr.UserCommands
{
    public record AddUserCommand(AddUserRequest user) : IRequest<HttpResponse<User>>
    {
    }
}
