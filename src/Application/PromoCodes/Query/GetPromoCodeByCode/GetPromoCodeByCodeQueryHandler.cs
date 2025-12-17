

using Application.Interfaces.UnitOfWorkInterfaces;
using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.PromoCodes.Query.GetPromoCodeByCode;

public class GetPromoCodeByCodeQueryHandler : IRequestHandler<GetPromoCodeByCodeQuery, ErrorOr<GetPromoCodeByCodeQueryResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    public GetPromoCodeByCodeQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<ErrorOr<GetPromoCodeByCodeQueryResponse>> Handle(GetPromoCodeByCodeQuery request, CancellationToken cancellationToken)
    {
        var promoCode = await _unitOfWork.PromoCodes
            .GetQueryable()
            .AsNoTracking()
            .Include(p => p.Usages)
            .FirstOrDefaultAsync(p => p.Code == request.Code, cancellationToken);
        if (promoCode == null)
        {
            return Error.NotFound("PromoCode.NotFound", "Promo code not found.");
        }
        var response = new GetPromoCodeByCodeQueryResponse(
           promoCode.Id,
           promoCode.Code!,
           promoCode.DiscountType,
           promoCode.DiscountValue,
           promoCode.ExpiryDate,
           promoCode.IsActive,
           promoCode.Usages.Count,
           promoCode.OwnerUserId,
           promoCode.Usages.Select(u => new PromoCodeUsageItem(
               u.Id,
               u.UserId,
               u.UsedAt
           )).ToList()
       );

        return response;
    }
}
