

using Domain.Enums;
using ErrorOr;
using MediatR;

namespace Application.PromoCodes.Query.GetPromoCodes;

public record GetPromoCodesQuery(
    int PageNumber,
    int PageSize
):IRequest<ErrorOr<GetPromoCodesQueryResponse>>;
public record GetPromoCodesQueryResponse(
    List<PromoCodeResponseItem> PromoCodes,
    int TotalCount
);
public record PromoCodeResponseItem(
    Guid Id,
    string Code,
    DiscountType DiscountType,
    decimal DiscountValue,
    DateTime ExpiryDate,
    int UsedCount,
    bool IsActive
);
