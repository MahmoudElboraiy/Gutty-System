
using ErrorOr;
using MediatR;

namespace Application.Plans.Queries.NewFolder1;
public record CategoryModificationDto(Guid CategoryId, uint? NumberOfMeals, uint? ProteinGrams);

public record CalculatePlanPriceQuery(
    Guid PlanId,
    uint? RiceCarbGrams,
    uint? PastaCarbGrams,
    string? PromoCode,
    string UserId,
List<CategoryModificationDto>? Categories
) : IRequest<ErrorOr<CalculatePlanPriceResponse>>;

public record CategoryCalculationItemDto(
    Guid CategoryId,
    string Name,
    uint NumberOfMeals,
    uint ProteinGrams,
    decimal CategoryPrice
);

public record CalculatePlanPriceResponse(
    decimal TotalPrice,
    decimal BreakfastPrice,
    decimal DinnerPrice,
    uint RiceCarbGrams,
    uint PastaCarbGrams,
    List<CategoryCalculationItemDto> Categories
);