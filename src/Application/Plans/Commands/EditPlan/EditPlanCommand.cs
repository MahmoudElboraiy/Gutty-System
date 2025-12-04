

using ErrorOr;
using MediatR;

namespace Application.Plans.Commands.EditPlan;

public record EditPlanCommand(
    Guid Id,
    string Name,
    string Description,
    string? ImageUrl,
    int DurationInDays,
    uint LMealsPerDay,
    uint BDMealsPerDay,
    decimal BreakfastPrice,
    decimal DinnerPrice,
    int CarbGrams,
    int MaxCarbGrams,
    List<EditPlanCategoryDto> LunchCategories
) : IRequest<ErrorOr<EditPlanCommandResponse>>;
public record EditPlanCategoryDto(
    int? Id,
    string Name,
    int NumberOfMeals,
    int ProteinGrams,
    decimal PricePerGram,
    bool AllowProteinChange,
    int MaxProteinGrams
);
public record EditPlanCommandResponse(
    Guid Id
);
