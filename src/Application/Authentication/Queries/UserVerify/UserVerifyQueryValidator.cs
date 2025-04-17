using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authentication.Queries.UserVerify
{
    public class UserVerifyQueryValidator:AbstractValidator<UserVerifyQuery>
    {
        public UserVerifyQueryValidator()
        {
            RuleFor(u => u.UserId).NotEmpty().WithMessage("معرّف المستخدم مطلوب");
        }
    }
}
