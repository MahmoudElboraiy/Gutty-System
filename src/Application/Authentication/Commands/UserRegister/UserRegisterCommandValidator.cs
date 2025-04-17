using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authentication.Commands.UserRegister
{
    public class UserRegisterCommandValidator : AbstractValidator<UserRegisterCommand>
    {
        public UserRegisterCommandValidator()
        {
            RuleFor(u => u.FirstName)
                .NotEmpty()
                .Length(2, 20).WithMessage("يجب أن يكون الاسم الأول بين 2 و 20 حرفًا");

            RuleFor(u => u.MiddleName)
                .NotEmpty()
                .Length(2, 20).WithMessage("يجب أن يكون الاسم الأوسط بين 2 و 20 حرفًا");

            RuleFor(u => u.LastName)
                .NotEmpty()
                .Length(2, 20).WithMessage("يجب أن يكون اسم العائلة بين 2 و 20 حرفًا");

            RuleFor(u => u.PhoneNumber)
                .NotEmpty()
                .Matches(@"^01[0-2,5]{1}[0-9]{8}$").WithMessage("رقم الهاتف غير صحيح");


            RuleFor(x => x.Password)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("كلمة المرور مطلوبة")
                .MinimumLength(8).WithMessage("كلمة المرور يجب أن تكون على الأقل 8 أحرف")
                .Matches("[A-Z]").WithMessage("يجب أن تحتوي على حرف كبير واحد على الأقل")
                .Matches("[0-9]").WithMessage("يجب أن تحتوي على رقم واحد على الأقل")
                .Matches("[^a-zA-Z0-9]").WithMessage("يجب أن تحتوي على رمز خاص واحد على الأقل");

            RuleFor(x => x.MainAddress)
                .NotEmpty().WithMessage("العنوان الرئيسي مطلوب");
        }
    }
}
