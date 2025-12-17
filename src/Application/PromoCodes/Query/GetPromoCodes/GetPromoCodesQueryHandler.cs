

using Application.Interfaces.UnitOfWorkInterfaces;
using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Application.PromoCodes.Query.GetPromoCodes;

public class GetPromoCodesQueryHandler : IRequestHandler<GetPromoCodesQuery, ErrorOr<GetPromoCodesQueryResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    public GetPromoCodesQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<ErrorOr<GetPromoCodesQueryResponse>> Handle(GetPromoCodesQuery request, CancellationToken cancellationToken)
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

        var response = new GetPromoCodesQueryResponse(
            request.PageNumber,
            request.PageSize,
            totalCount,
            promoCodes
        );

        return response;

    }
}
