using MediatR;
using MovieLibrary.DL.Interfaces;
using MovieLibrary.Models.Mediatr.WatchedMoviesCommands;
using MovieLibrary.Models.MongoDbModels;
using MovieLibrary.Models.Responses;

namespace MovieLibrary.BL.CommandHandlers.WatchedMoviesListCommandHandler
{
    public class DeleteWatchedMoviesCommandHandler : IRequestHandler<DeleteWatchedMoviesCommand, HttpResponse<WatchedList>>
    {
        private readonly IWatchedMoviesRepository _watchedMoviesRepository;

        public DeleteWatchedMoviesCommandHandler(IWatchedMoviesRepository watchedMoviesRepository)
        {
            _watchedMoviesRepository = watchedMoviesRepository;
        }

        public async Task<HttpResponse<WatchedList>> Handle(DeleteWatchedMoviesCommand request, CancellationToken cancellationToken)
        {
            if (request.userId <= 0)
            {
                return new HttpResponse<WatchedList>
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Message = StaticResponses.UserIdLessThanOrEqualTo0,
                    Value = null
                };
            }
            var watchedListToDelete = await _watchedMoviesRepository.GetWatchedMovies(request.userId);
            var firstOne = watchedListToDelete.FirstOrDefault();
            if (firstOne == null)
            {
                return new HttpResponse<WatchedList>
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Message = "There is no such list to be deleted",
                    Value = null
                };
            }
            await _watchedMoviesRepository.DeleteWatchedMovie(request.userId);
            return new HttpResponse<WatchedList>
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Message = "Successfully deleted watched movies list",
                Value = firstOne
            };
        }
    }
}
