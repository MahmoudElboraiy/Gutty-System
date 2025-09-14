using FluentValidation;

namespace Application.Meals.Commands.UpdateMeal;

public class UpdateMealCommandValidator : AbstractValidator<UpdateMealCommand>
{
    public UpdateMealCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEqual(Guid.Empty)
            .WithMessage("معرف الوجبة مطلوب");

        RuleFor(x => x.ItemId)
            .NotEqual(Guid.Empty)
            .WithMessage("معرف العنصر مطلوب");

        RuleFor(x => x.Weight)
            .GreaterThan(0)
            .WithMessage("الوزن يجب أن يكون أكبر من صفر");

        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0)
            .WithMessage("السعر لا يمكن أن يكون سالباً");

        RuleFor(x => x.Quantity)
            .GreaterThan(0u)
            .WithMessage("الكمية يجب أن تكون أكبر من صفر");

        RuleFor(x => x.MealType)
            .IsInEnum()
            .WithMessage("نوع الوجبة غير صالح");
    }
}