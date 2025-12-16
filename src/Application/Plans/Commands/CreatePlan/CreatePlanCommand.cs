using Domain.Enums;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Plans.Commands.CreatePlan;

public record CreatePlanCommand(
    string Name,
    string Description,
    IFormFile Image,
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
    uint MaxProteinGrams
);
public record CreatePlanCommandResponse(Guid Id);