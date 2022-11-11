using FluentValidation;
using MovieLibrary.Models.Requests.SubscriptionRequests;

namespace Movie_Library_Final_Project.Validators.SubscriptionRequestsValidator
{
    public class UpdateSubsRequestValidator : AbstractValidator<UpdateSubscriptionRequest>
    {
        public UpdateSubsRequestValidator()
        {
            RuleFor(x => x.UserId)
               .NotEmpty().WithMessage("Cant assign a subscription to an user without Id")
               .GreaterThanOrEqualTo(0).WithMessage("There are no negative Id's");
            RuleFor(x => x.PlanId)
               .NotEmpty().WithMessage("There are no plans without an Id")
               .GreaterThanOrEqualTo(0).WithMessage("There are no negative Id's");
            RuleFor(x => x.ValidTill)
                .NotEmpty()
                .GreaterThan(DateTime.MinValue).WithMessage("We can't work with such dates")
                .LessThan(DateTime.MaxValue).WithMessage("We can't work with such dates");
            RuleFor(x => x.CreatedAt)
                .NotEmpty()
                .GreaterThan(DateTime.MinValue).WithMessage("We can't work with such dates")
                .LessThan(DateTime.MaxValue).WithMessage("We can't work with such dates");
            RuleFor(x => x.SubscriptionId)
                .NotEmpty().WithMessage("Cant find a subscription without an Id")
               .GreaterThanOrEqualTo(0).WithMessage("There are no negative Id's");
        }
    }
}
