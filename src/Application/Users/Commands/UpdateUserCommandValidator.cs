using Domain.Models.Identity;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace Application.Users.Commands
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {

        public UpdateUserCommandValidator(UserManager<User> userManager)
        {

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(255);


            RuleFor(x => x.MainAddress)
                .NotEmpty().WithMessage("Main address is required.")
                .MaximumLength(255);

            RuleFor(x => x.SecondaryAddress)
                .MaximumLength(255);

            RuleFor(x => x.SecondPhoneNumber)
                .NotEmpty()
                .Length(11).WithMessage("Phone number must be exactly 11 digits.")
                .Matches(@"^01[0-9]{9}$").WithMessage("Phone number must be a valid Egyptian number.");
        }

    }

}
