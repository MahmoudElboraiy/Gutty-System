using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Application.Authentication.Queries.UserLogin
{
    public class UserLoginQueryValidator : AbstractValidator<UserLoginQuery>
    {
        public UserLoginQueryValidator()
        {
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("رقم الهاتف مطلوب");

            RuleFor(x => x.Password).NotEmpty().WithMessage("كلمة المرور مطلوبة");
        }
    }
}
