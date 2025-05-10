using Domain.Enums;
using ErrorOr;
using MediatR;

namespace Application.Plans.Commands.CreatePlan;

public record CreatePlanCommand(
    string Name,
    string Description,
    bool IsPreDefined,
    List<CreatePlanMeal> Meals
) : IRequest<ErrorOr<CreatePlanCommandResponse>>;

public record CreatePlanMeal(Guid ItemId, int? ExtraItemOptionId, MealType MealType, int Quantity);

public record CreatePlanCommandResponse(Guid Id);
