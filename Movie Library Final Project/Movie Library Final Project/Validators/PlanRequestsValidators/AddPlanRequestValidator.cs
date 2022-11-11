using FluentValidation;
using MovieLibrary.Models.Requests.PlanRequests;

namespace Movie_Library_Final_Project.Validators.PlanRequestsValidators
{
    public class AddPlanRequestValidator : AbstractValidator<AddPlanRequest>
    {
        public AddPlanRequestValidator()
        {
            RuleFor(x => x.Type)
                .NotEmpty().WithMessage("Cant input empty Type")
                .MinimumLength(2).WithMessage("Type input is too little")
                .MaximumLength(30).WithMessage("Type input is too big");

            RuleFor(x => x.PricePerMonth)
                .NotEmpty().WithMessage("There are no free plans")
                .GreaterThanOrEqualTo(10).WithMessage("10 bucks is the cheapest plan")
                .LessThanOrEqualTo(100).WithMessage("100 can be the priciest plant");
        }
    }
}
