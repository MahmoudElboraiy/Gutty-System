using Domain.Enums;
using ErrorOr;
using MediatR;

namespace Application.Plans.Commands.CreatePlan;

public record CreatePlanCommand(
    string Name,
    string Description,
    uint DurationInDays,
    uint LunchMealsPerDay,
    uint DinnerMealsPerDay,
    uint BreakfastMealsPerDay,
    uint MaxSeaFood,
    uint MaxMeat,
    uint MaxTwagen,
    uint MaxChicken,
    uint MaxPizza,
    uint MaxHighCarb,
    decimal Price
) : IRequest<ErrorOr<CreatePlanCommandResponse>>;

public record CreatePlanCommandResponse(Guid Id);