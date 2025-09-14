using Application.Meals.Commands.Create;
using FluentValidation;

namespace Application.Meals.Commands.CreateMeal;

public class CreateMealCommandValidator : AbstractValidator<CreateMealCommand>
{
    public CreateMealCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("اسم الوجبة مطلوب")
            .MaximumLength(100).WithMessage("اسم الوجبة لا يمكن أن يتجاوز 100 حرف")
            .MinimumLength(2).WithMessage("اسم الوجبة يجب أن يكون على الأقل حرفين");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("وصف الوجبة مطلوب")
            .MaximumLength(500).WithMessage("الوصف لا يمكن أن يتجاوز 500 حرف")
            .MinimumLength(10).WithMessage("الوصف يجب أن يكون على الأقل 10 أحرف");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("السعر يجب أن يكون أكبر من صفر")
            .LessThan(1000).WithMessage("السعر لا يمكن أن يتجاوز 1000");

        // Make ImageUrl optional or remove if not in your command
        When(x => !string.IsNullOrEmpty(x.ImageUrl), () =>
        {
            RuleFor(x => x.ImageUrl)
                .MaximumLength(500).WithMessage("رابط الصورة لا يمكن أن يتجاوز 500 حرف")
                .Must(BeAValidUrl).WithMessage("رابط الصورة يجب أن يكون رابطاً صالحاً");
        });

        RuleFor(x => x.MealCategoryId)
            .GreaterThan(0).WithMessage("معرف فئة الوجبة مطلوب");

        RuleFor(x => x.PreparationTime)
            .GreaterThan(0).WithMessage("وقت التحضير يجب أن يكون أكبر من صفر")
            .LessThanOrEqualTo(300).WithMessage("وقت التحضير لا يمكن أن يتجاوز 300 دقيقة");

        RuleFor(x => x.Calories)
            .GreaterThan(0).WithMessage("السعرات الحرارية يجب أن تكون أكبر من صفر")
            .LessThanOrEqualTo(5000).WithMessage("السعرات الحرارية لا يمكن أن تتجاوز 5000");

        // Only validate Foods if it exists in your command
        RuleFor(x => x.Foods)
            .NotEmpty().WithMessage("عنصر طعام واحد على الأقل مطلوب")
            .Must(foods => foods != null && foods.Count <= 20).WithMessage("لا يمكن تجاوز 20 عنصر طعام لكل وجبة");

        RuleForEach(x => x.Foods).SetValidator(new MealFoodItemValidator());
    }

    private bool BeAValidUrl(string url)
    {
        if (string.IsNullOrEmpty(url))
            return true;

        return Uri.TryCreate(url, UriKind.Absolute, out var uriResult)
               && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
    }
}

public class MealFoodItemValidator : AbstractValidator<MealFoodItem>
{
    public MealFoodItemValidator()
    {
        RuleFor(x => x.FoodId)
            .GreaterThan(0).WithMessage("معرف الطعام مطلوب");

        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("كمية الطعام يجب أن تكون أكبر من صفر")
            .LessThanOrEqualTo(100).WithMessage("كمية الطعام لا يمكن أن تتجاوز 100");
    }
}