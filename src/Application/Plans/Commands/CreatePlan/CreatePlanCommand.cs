using Domain.Enums;
using ErrorOr;
using MediatR;

namespace Application.Plans.Commands.CreatePlan;

public record CreatePlanCommand(
    string Name,
    string Description,
    uint DurationInDays,
    decimal BreakfastPrice,
    decimal DinnerPrice,
    uint RiceCarbGrams,
    uint PastaCarbGrams,
    uint MaxRiceCarbGrams,
    uint MaxPastaCarbGrams,
    List<PlanCategoryDto> LunchCategories
) : IRequest<ErrorOr<CreatePlanCommandResponse>>;
public record PlanCategoryDto(
    string Name,
    uint NumberOfMeals,
    uint ProteinGrams,
    decimal PricePerGram,
    bool AllowProteinChange,
    uint MaxMeals,
    uint MaxProteinGrams
);
public record CreatePlanCommandResponse(Guid Id);