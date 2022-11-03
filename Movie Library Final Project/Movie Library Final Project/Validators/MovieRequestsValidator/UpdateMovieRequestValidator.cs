using FluentValidation;
using MovieLibrary.Models.Requests.MovieRequests;

namespace Movie_Library_Final_Project.Validators.MovieRequestsValidator
{
    public class UpdateMovieRequestValidator : AbstractValidator<UpdateMovieRequest>
    {
        public UpdateMovieRequestValidator()
        {
            RuleFor(x => x.MovieId)
                .NotEmpty().WithMessage("Cant update a movie without an Id")
                .GreaterThanOrEqualTo(0).WithMessage("There are no negative Id's");
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Your title cant be empty")
                .MinimumLength(2).WithMessage("There are not movies with shorter name than 2")
                .MaximumLength(100).WithMessage("100 char's is the maximum length of a movie");
            RuleFor(x => x.LengthInMinutes)
                .NotEmpty().WithMessage("There are no movies without runtime")
                .GreaterThanOrEqualTo(1).WithMessage("Movie must have atleast a minute lenght")
                .LessThanOrEqualTo(1000).WithMessage("No movies with runtime greater than 1000 minutes");
            RuleFor(x => x.ReleaseYear)
                .NotEmpty().WithMessage("Movie year can't be empty")
                .GreaterThanOrEqualTo(1930).WithMessage("There are no movies before 1930");
            RuleFor(x => x.Genre)
                .NotEmpty().WithMessage("Every movie has a genre")
                .MinimumLength(2).WithMessage("There ar eno movies with less than 2 char's");
        }
    }
}
