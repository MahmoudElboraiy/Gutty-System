using ErrorOr;
using MediatR;

namespace Application.Plans.Queries.GetPlans;

public record GetPlansQuery(int pageNumber , int pageSize) : IRequest<GetPlansQueryResponse>;

public record GetPlansQueryResponse(List<GetPlanQueryResponseItem> Plans);
public record GetPlanCategoryResponseItem(
    int Id,
    string Name,
    uint NumberOfMeals,
    uint ProteinGrams,
    decimal PricePerGram,
    bool AllowProteinChange,
    uint MaxProteinGrams,
    decimal CategoryPrice
);
public record GetPlanQueryResponseItem(
    Guid Id,
    string Name,
    string Description,
    string? ImageUrl,
    uint DurationInDays,
    uint LMealsPerDay,
    uint BDMealsPerDay,
    decimal BreakfastPrice,
    decimal DinnerPrice,
    decimal TotalPrice,
    uint CarbGrams,
    uint MaxCarbGrams,
     List<GetPlanCategoryResponseItem> Categories
)
{
  
}
