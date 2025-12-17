
using ErrorOr;
using MediatR;

namespace Application.Plans.Queries.NewFolder1;
public record CategoryModificationDto(int CategoryId, uint? NumberOfMeals, uint? ProteinGrams);

public record CalculatePlanPriceQuery(
    Guid PlanId,
    uint? CarbGrams,
    string? PromoCode,
List<CategoryModificationDto>? Categories
) : IRequest<ErrorOr<CalculatePlanPriceResponse>>;

public record CalculatePlanPriceResponse(
    decimal TotalPrice
    //decimal BreakfastPrice,
    //decimal DinnerPrice,
    //uint CarbGrams,
    //List<CategoryCalculationItemDto> Categories
);

public record CategoryCalculationItemDto(
    int CategoryId,
    string Name,
    uint NumberOfMeals,
    uint ProteinGrams,
    decimal CategoryPrice
);
