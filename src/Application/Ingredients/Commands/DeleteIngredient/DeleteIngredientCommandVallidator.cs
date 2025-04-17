using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Ingredients.Commands.DeleteIngredient
{
    class DeleteIngredientCommandVallidator:AbstractValidator<DeleteIngredientCommand>
    {
        public DeleteIngredientCommandVallidator()
        {
            RuleFor(x => x.Id)
           .GreaterThan(0)
           .WithMessage("الرقم التعريفي غير صالح، يجب أن يكون أكبر من صفر");
        }
    }
}
