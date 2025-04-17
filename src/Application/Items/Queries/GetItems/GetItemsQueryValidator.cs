using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Items.Queries.GetItems
{
    public class GetItemsQueryValidator:AbstractValidator<GetItemsQuery>
    {
        public GetItemsQueryValidator()
        {
            RuleFor(x => x.SearchText)
                .MaximumLength(100).WithMessage("نص البحث لا يجب أن يتجاوز 100 حرف");
        }
    }
}
