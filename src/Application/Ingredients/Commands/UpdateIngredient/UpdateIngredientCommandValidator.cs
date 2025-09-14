using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Application.Ingredients.Commands.UpdateIngredient
{
    public class UpdateIngredientCommandValidator : AbstractValidator<UpdateIngredientCommand>
    {
        public UpdateIngredientCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("الرقم التعريفي غير صالح، يجب أن يكون أكبر من صفر");

            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("الاسم مطلوب")
                .Length(2, 50)
                .WithMessage("الاسم يجب أن يكون بين 2 و 50 حرفًا");

            RuleFor(x => x.Stock)
                .GreaterThanOrEqualTo(0)
                .WithMessage("الكمية يجب أن تكون صفر أو أكثر");
        }
    }
}
