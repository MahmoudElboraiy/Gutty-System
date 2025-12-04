using Domain.Enums;
using ErrorOr;
using MediatR;

namespace Application.Plans.Commands.CreatePlan;

public record CreatePlanCommand(
    string Name,
    string Description,
    string? ImageUrl,
    uint DurationInDays,
    uint LMealsPerDay,
    uint BDMealsPerDay,
    decimal BreakfastPrice,
    decimal DinnerPrice,
    uint CarbGrams,
    uint MaxCarbGrams,
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