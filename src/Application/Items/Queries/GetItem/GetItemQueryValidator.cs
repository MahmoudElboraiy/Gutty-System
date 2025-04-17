using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Items.Queries.GetItem
{
    public class GetItemQueryValidator:AbstractValidator<GetItemQuery>
    {
        public GetItemQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("رقم المعرف غير صالح.");
        }
    }
}
