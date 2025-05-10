using FluentValidation;

namespace Application.Plans.Commands.CreatePlan;

public class CreatePlanCommandValidator : AbstractValidator<CreatePlanCommand>
{
    public CreatePlanCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(255);

        RuleFor(x => x.Description).NotEmpty().MaximumLength(1000);

        RuleFor(x => x.Meals).NotEmpty().WithMessage("At least one meal is required");

        RuleForEach(x => x.Meals)
            .ChildRules(meal =>
            {
                meal.RuleFor(x => x.ItemId).NotEmpty();

                meal.RuleFor(x => x.Quantity)
                    .GreaterThan(0)
                    .WithMessage("Quantity must be greater than 0");

                meal.When(
                    x => x.ExtraItemOptionId.HasValue,
                    () =>
                    {
                        meal.RuleFor(x => x.ExtraItemOptionId)
                            .GreaterThan(0)
                            .WithMessage("ExtraItemOptionId must be greater than 0");
                    }
                );
            });
    }
}
