using FluentValidation;

namespace Application.Meals.Commands.DeleteMeal;

public class DeleteMealCommandValidator : AbstractValidator<DeleteMealCommand>
{
    public DeleteMealCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Meal ID is required.");
    }
}