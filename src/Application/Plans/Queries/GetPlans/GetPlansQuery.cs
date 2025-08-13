using ErrorOr;
using MediatR;

namespace Application.Plans.Queries.GetPlans;

public record GetPlansQuery() : IRequest<GetPlansQueryResponse>;

public record GetPlansQueryResponse(List<GetPlanQueryResponseItem> Plans);
public record GetPlanCategoryResponseItem(
    Guid Id,
    string Name,
    uint NumberOfMeals,
    uint ProteinGrams,
    decimal PricePerGram,
    bool AllowProteinChange,
    uint MaxMeals,
    uint MaxProteinGrams,
    decimal CategoryPrice
);
public record GetPlanQueryResponseItem(
    Guid Id,
    string Name,
    string Description,
    uint DurationInDays,
    decimal BreakfastPrice,
    decimal DinnerPrice,
    decimal TotalPrice,
    uint RiceCarbGrams,
    uint PastaCarbGrams,
    uint MaxRiceCarbGrams,
    uint MaxPastaCarbGrams,
     List<GetPlanCategoryResponseItem> Categories
)
{
  
}
