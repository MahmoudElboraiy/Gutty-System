using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Ingredients.Commands.CreateIngredient
{
    public class CreateIngredientCommandValidator:AbstractValidator<CreateIngredientCommand>
    {
        public CreateIngredientCommandValidator()
        {
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("اسم المكوّن مطلوب")
                .Length(2, 50).WithMessage("اسم المكوّن يجب أن يكون بين 2 و 50 حرفًا");

            RuleFor(x => x.Stock)
                .GreaterThanOrEqualTo(0).WithMessage("المخزون لا يمكن أن يكون رقمًا سالبًا");
        }
    }
}
