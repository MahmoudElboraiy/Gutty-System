using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Application.Items.Commands.UpdateItem
{
    public class UpdateItemCommandValidator : AbstractValidator<UpdateItemCommand>
    {
        public UpdateItemCommandValidator()
        {
            RuleFor(x => x.Id).NotEqual(Guid.Empty).WithMessage("المُعرّف غير صالح");

            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("الاسم مطلوب")
                .Length(2, 50)
                .WithMessage("الاسم يجب أن يكون بين 2 و 50 حرفًا");

            RuleFor(x => x.Description)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("الوصف مطلوب")
                .Length(5, 250)
                .WithMessage("الوصف يجب أن يكون بين 5 و 250 حرفًا");

            RuleFor(x => x.WeightRaw)
                .GreaterThanOrEqualTo(0)
                .WithMessage("الوزن الخام لا يمكن أن يكون سالبًا");

            RuleFor(x => x.Weight)
                .GreaterThanOrEqualTo(0)
                .WithMessage("الوزن لا يمكن أن يكون سالبًا");

            RuleFor(x => x.Calories)
                .GreaterThanOrEqualTo(0)
                .WithMessage("السعرات الحرارية لا يمكن أن تكون سالبة");

            RuleFor(x => x.Fats)
                .GreaterThanOrEqualTo(0)
                .WithMessage("الدهون لا يمكن أن تكون سالبة");

            RuleFor(x => x.Carbohydrates)
                .GreaterThanOrEqualTo(0)
                .WithMessage("الكربوهيدرات لا يمكن أن تكون سالبة");

            RuleFor(x => x.Proteins)
                .GreaterThanOrEqualTo(0)
                .WithMessage("البروتينات لا يمكن أن تكون سالبة");

            RuleFor(x => x.RecipeIngredients)
                .NotNull()
                .WithMessage("مكونات الوصفة مطلوبة")
                .Must(list => list.Count > 0)
                .WithMessage("يجب إضافة مكون واحد على الأقل");
        }
    }
}
