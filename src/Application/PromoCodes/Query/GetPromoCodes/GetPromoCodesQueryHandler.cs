

using Application.Interfaces.UnitOfWorkInterfaces;
using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
        int skip = (request.PageNumber - 1) * request.PageSize;
        int take = request.PageSize;

        var promoCodes = await _unitOfWork.PromoCodes
            .GetQueryable()
            .AsNoTracking()
            .Include(p => p.Usages)          
            .OrderByDescending(p => p.ExpiryDate)   
            .Skip(skip)
            .Take(take)
            .Select(p => new PromoCodeResponseItem(
                p.Id,
                p.Code!,
                p.DiscountType,
                p.DiscountValue,
                p.ExpiryDate,
                p.Usages.Count,      
                p.IsActive
            ))
            .ToListAsync(cancellationToken);

        int totalCount = promoCodes.Count;
        var response = new GetPromoCodesQueryResponse(promoCodes, totalCount);
        return response;


    }
}
