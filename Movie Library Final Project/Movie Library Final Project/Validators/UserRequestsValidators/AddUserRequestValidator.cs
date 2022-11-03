using FluentValidation;
using MovieLibrary.Models.Requests.UserRequests;

namespace Movie_Library_Final_Project.Validators.UserRequestsValidators
{
    public class AddUserRequestValidator : AbstractValidator<AddUserRequest>
    {
        public AddUserRequestValidator()
        {
            RuleFor(x => x.Age)
                .NotEmpty()
                .GreaterThan(0).WithMessage("You can't be below 0 years old.")
                .LessThanOrEqualTo(150).WithMessage("You can't be older than 150");
            RuleFor(x => x.Name)
                .NotEmpty()
                .MinimumLength(2).WithMessage("Your name cant be below 2 charachters")
                .MaximumLength(100).WithMessage("This name is too long");
        }

    }
}
