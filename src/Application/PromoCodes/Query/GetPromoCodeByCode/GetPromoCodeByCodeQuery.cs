

using Domain.Enums;
using ErrorOr;
using MediatR;

namespace Application.PromoCodes.Query.GetPromoCodeByCode;

public record GetPromoCodeByCodeQuery(string Code):IRequest<ErrorOr<GetPromoCodeByCodeQueryResponse>>;
public record GetPromoCodeByCodeQueryResponse(
    Guid Id,
    string Code,
    DiscountType DiscountType,
    decimal DiscountPercentage,
    DateTime ExpiryDate,
    bool IsActive, 
    int UsedCount,
    string? OwnerUserId,
     List<PromoCodeUsageItem> Usages
    );
public record PromoCodeUsageItem(
    Guid Id,
    string? UsedByUserId,
    DateTime UsedAt
);
