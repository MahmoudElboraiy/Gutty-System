using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IngredientLogs.Queries.GetIngredientLogs
{
    public class GetIngredientLogsQueryValidator:AbstractValidator<GetIngredientLogsQuery>
    {
        public GetIngredientLogsQueryValidator()
        {
            RuleFor(x => x.IngredientId)
            .Cascade(CascadeMode.Stop)
            .Must(id => id == null || id > 0)
            .WithMessage("معرّف المكوّن يجب أن يكون رقمًا موجبًا");

            RuleFor(x => x.PageNumber)
                .Cascade(CascadeMode.Stop)
                .Must(p => p == null || p > 0)
                .WithMessage("رقم الصفحة يجب أن يكون أكبر من 0");

            RuleFor(x => x.PageSize)
                .Cascade(CascadeMode.Stop)
                .Must(p => p == null || p > 0)
                .WithMessage("عدد العناصر في الصفحة يجب أن يكون أكبر من 0");
        }
    }
}
