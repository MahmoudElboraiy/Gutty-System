

using Domain.Enums;
using ErrorOr;
using MediatR;

namespace Application.PromoCodes.Query.GetPromoCodes;

public record GetPromoCodesQuery(
    int PageNumber,
    int PageSize,
    string? SearchName = null,
    bool? IsActive = null
) :IRequest<ErrorOr<GetPromoCodesQueryResponse>>;
public record GetPromoCodesQueryResponse(
    int pageNumber,
    int pageSize,
    int TotalCount,
    List<PromoCodeItem> PromoCodes
);
public record PromoCodeItem(
    Guid Id,
    string Code,
    DiscountType DiscountType,
    decimal DiscountValue,
    DateTime ExpiryDate,
    int UsedCount,
    bool IsActive
);
