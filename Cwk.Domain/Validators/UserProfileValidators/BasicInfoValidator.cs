using Cwk.Domain.Aggregates.UserProfileAggregate;
using FluentValidation;

namespace Cwk.Domain.Validators.UserProfileValidators
{
    public class BasicInfoValidator: AbstractValidator<BasicInfo>
    {
        public BasicInfoValidator()
        {
            RuleFor(info=> info.FirstName).NotNull()
                .WithMessage("First name is a required field!")
                .MinimumLength(3).WithMessage("First name must be at least 3 characters long")
                .MaximumLength(50).WithMessage("First name can not extend 50 characters length");
            RuleFor(info => info.LastName).NotNull()
               .WithMessage("Last name is a required field!")
               .MinimumLength(3).WithMessage("Last name must be at least 3 characters long")
               .MaximumLength(50).WithMessage("Last name can not extend 50 characters length");
            RuleFor(info => info.EmailAddress).NotNull()
                .WithMessage("Email address is required")
                .EmailAddress()
                .WithMessage("Provided string is not a valid email");
            //RuleFor(info => info.DateOfBirth)
            //    .InclusiveBetween(new DateTime(DateTime.Now.AddYears(-125).Year), new DateTime(DateTime.Now.AddYears(125).Year))
            //    .WithMessage("Age must be between 18 and 125");
        }
    }
}
