

using Application.Cache;
using Application.Interfaces;
using Application.Interfaces.UnitOfWorkInterfaces;
using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Application.PromoCodes.Query.GetPromoCodes;

public class GetPromoCodesQueryHandler : IRequestHandler<GetPromoCodesQuery, ErrorOr<GetPromoCodesQueryResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICacheService _cacheService;
    public GetPromoCodesQueryHandler(IUnitOfWork unitOfWork, ICacheService cacheService)
    {
        _unitOfWork = unitOfWork;
        _cacheService = cacheService;
    }
    public async Task<ErrorOr<GetPromoCodesQueryResponse>> Handle(GetPromoCodesQuery request, CancellationToken cancellationToken)
    {
        string parametersKey = $"search_{request.SearchName}_active_{request.IsActive}";

        var allPromoCodes = await _cacheService.GetOrCreateAsync<List<PromoCodeItem>>(
            baseKey: CacheKeys.PromoCodesAll,
            versionKey: CacheKeys.PromoCodesVersion,
            parametersKey: parametersKey,
            factory: async () =>
            {
                var query = _unitOfWork.PromoCodes
            .GetQueryable()
            .AsNoTracking()
            .Include(p => p.Usages)
            .AsQueryable();

                if (!string.IsNullOrWhiteSpace(request.SearchName))
                {
                    query = query.Where(p =>
                        p.Code.Contains(request.SearchName)
                    );
                }

                if (request.IsActive.HasValue)
                {
                    query = query.Where(p => p.IsActive == request.IsActive.Value);
                }

                int totalCount = await query.CountAsync(cancellationToken);

                int skip = (request.PageNumber - 1) * request.PageSize;

                var promoCodes = await query
                    .OrderByDescending(p => p.ExpiryDate)
                    .Skip(skip)
                    .Take(request.PageSize)
                    .Select(p => new PromoCodeItem(
                        p.Id,
                        p.Code!,
                        p.DiscountType,
                        p.DiscountValue,
                        p.ExpiryDate,
                        p.Usages.Count,
                        p.IsActive
                    ))
                    .ToListAsync(cancellationToken);

                return promoCodes;
            });

        int totalCount = allPromoCodes.Count;
        var pagedPromoCodes = allPromoCodes
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToList();

        return new GetPromoCodesQueryResponse(
            request.PageNumber,
            request.PageSize,
            totalCount,
            pagedPromoCodes
        );

    }
}
