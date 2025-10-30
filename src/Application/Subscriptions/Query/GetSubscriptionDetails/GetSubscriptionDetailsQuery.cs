

using ErrorOr;
using MediatR;

namespace Application.Subscriptions.Query.GetSubscriptionDetails;

public record GetSubscriptionDetailsQuery: IRequest<GetSubscriptionDetailsQueryResponse>;
public record GetSubscriptionDetailsQueryResponse(
    bool IsSuccess,
    string PlanName,
    bool IsPaused,
    DateTime StartDate,
    uint DaysLeft,
    uint LunchMealsLeft,
    uint CarbGrams,
     List<LunchCategoryResponse> LunchCategories
);
public record LunchCategoryResponse(
   string SubCategoryName,
    uint NumberOfMeals,
    uint NumberOfMealsLeft,
    uint ProteinGrams
);


