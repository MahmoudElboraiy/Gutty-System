using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Ingredients.Queries.GetIngredients
{
    public class GetIngredientsQueryValidator:AbstractValidator<GetIngredientsQuery>
    {
        public GetIngredientsQueryValidator()
        {
            RuleFor(x => x.SearchTerm)
                .MaximumLength(100)
                .WithMessage("كلمة البحث يجب ألا تزيد عن 100 حرف");
        }
    }
}
