

using Application.Cache;
using Application.Interfaces;
using Application.Interfaces.UnitOfWorkInterfaces;
using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.PromoCodes.Query.GetPromoCodeByCode;

public class GetPromoCodeByCodeQueryHandler : IRequestHandler<GetPromoCodeByCodeQuery, ErrorOr<GetPromoCodeByCodeQueryResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICacheService _cacheService;
    public GetPromoCodeByCodeQueryHandler(IUnitOfWork unitOfWork,ICacheService cacheService)
    {
        _unitOfWork = unitOfWork;
        _cacheService = cacheService;
    }
    public async Task<ErrorOr<GetPromoCodeByCodeQueryResponse>> Handle(GetPromoCodeByCodeQuery request, CancellationToken cancellationToken)
    {
        var cacheKeyParams = $"promoCode_{request.Code}";
        return await _cacheService.GetOrCreateAsync<
            ErrorOr<GetPromoCodeByCodeQueryResponse>>(
            baseKey: CacheKeys.PromoCodeById,
            versionKey: CacheKeys.PromoCodesVersion,
            parametersKey: cacheKeyParams,
            factory: async () =>
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
            });

    }
}
