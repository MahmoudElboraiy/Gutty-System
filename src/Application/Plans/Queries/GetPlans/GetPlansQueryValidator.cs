using FluentValidation;

namespace Application.Plans.Queries.GetPlans;

public class GetPlansQueryValidator : AbstractValidator<GetPlansQuery>
{
    public GetPlansQueryValidator()
    {
        // No validation rules needed for a simple GetPlans query without parameters
    }
} 