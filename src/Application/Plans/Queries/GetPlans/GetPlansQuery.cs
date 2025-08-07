using ErrorOr;
using MediatR;

namespace Application.Plans.Queries.GetPlans;

public record GetPlansQuery() : IRequest<GetPlansQueryResponse>;

public record GetPlansQueryResponse(List<GetPlanQueryResponseItem> Plans);

public record GetPlanQueryResponseItem(
    Guid Id,
    string Name,
    string Description,
    uint MaxSeaFood,
    uint MaxMeat,
    uint MaxTwagen,
    uint MaxChicken,
    uint MaxPizza,
    uint MaxHighCarb,
    decimal Price
); 