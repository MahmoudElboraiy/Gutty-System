using FluentValidation;

namespace Application.Meals.Queries.GetMeal;

public class GetMealQueryValidator : AbstractValidator<GetMealQuery>
{
    public GetMealQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEqual(Guid.Empty)
            .WithMessage("معرف الوجبة مطلوب");
    }
}