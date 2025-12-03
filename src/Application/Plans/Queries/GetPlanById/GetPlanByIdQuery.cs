

using Application.Plans.Queries.GetPlans;
using ErrorOr;
using MediatR;

namespace Application.Plans.Queries.GetPlanById
{
    public record GetPlanByIdQuery(Guid PlanId) : IRequest<ErrorOr<GetPlanByIdQueryResponse>>;
    public record GetPlanByIdQueryResponse(
           Guid Id,
    string Name,
    string Description,
    uint DurationInDays,
    uint LMealsPerDay,
    uint BDMealsPerDay,
    decimal BreakfastPrice,
    decimal DinnerPrice,
    decimal TotalPrice,
    uint CarbGrams,
    uint MaxCarbGrams,
     List<GetPlanCategoryByIdResponseItem> Categories
    );
    public record GetPlanCategoryByIdResponseItem(
        int Id,
        string Name,
        uint NumberOfMeals,
        uint ProteinGrams,
        decimal PricePerGram,
        bool AllowProteinChange,
        uint MaxProteinGrams,
        decimal CategoryPrice
    );
}
