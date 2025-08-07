using FluentValidation;

namespace Application.Plans.Commands.CreatePlan;

public class CreatePlanCommandValidator : AbstractValidator<CreatePlanCommand>
{
    public CreatePlanCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(255);

        RuleFor(x => x.Description).NotEmpty().MaximumLength(1000);

        RuleFor(x => x.Price).GreaterThan(0);
    }
}
