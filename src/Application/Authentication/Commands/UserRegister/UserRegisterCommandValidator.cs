using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Application.Authentication.Commands.UserRegister
{
    public class UserRegisterCommandValidator : AbstractValidator<UserRegisterCommand>
    {
        public UserRegisterCommandValidator()
        {
            RuleFor(u => u.Name)
                .NotEmpty()
                .Length(2, 100)
                .WithMessage("The Name must be more than to character");


            RuleFor(u => u.PhoneNumber)
                .NotEmpty()
                .Matches(@"^01[0-2,5]{1}[0-9]{8}$")
                .WithMessage("Not valid number");

            RuleFor(x => x.Password)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Password is empty")
                .MinimumLength(8)
                .WithMessage("Password must be more than 8 character")
                .Matches("[A-Z]")
                .WithMessage("Password must has at least one Captial Letter")
                .Matches("[0-9]")
                .WithMessage("Password must has at least one number")
                .Matches("[^a-zA-Z0-9]")
                .WithMessage("Password must has at least one number");

            RuleFor(x => x.MainAddress).NotEmpty().WithMessage("Main address is required");
        }
    }
}
