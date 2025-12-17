

using Application.Interfaces.UnitOfWorkInterfaces;
using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Application.Inventory.Purchases.Query.GetPurchasesByDays;

public class GetPurchasesByDaysQueryHandler : IRequestHandler<GetPurchasesByDaysQuery, ErrorOr<GetPurchasesByDaysResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    public GetPurchasesByDaysQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<ErrorOr<GetPurchasesByDaysResponse>> Handle(GetPurchasesByDaysQuery request, CancellationToken cancellationToken)
    {
        var cutoffDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-request.Days));

        var purchases = _unitOfWork.Purchases
            .GetQueryable()
            .AsNoTracking()
            .Where(p => p.PurchaseDate >= cutoffDate);
        if (!string.IsNullOrWhiteSpace(request.searchName))
        {
            var search = request.searchName.ToLower();
            purchases = purchases.Where(p => p.ItemName.ToLower().Contains(search));
           
        }
        int totalCount = await purchases.CountAsync(cancellationToken);
        int skip = (request.PageNumber - 1) * request.PageSize;

        var purchasesAfterSkip = await purchases
            .OrderByDescending(p => p.PurchaseDate)
            .Skip(skip)
            .Take(request.PageSize)
            .Select(p => new GetPurchasesByDaysItem(
                p.Id,
                p.ItemName,
                p.Quantity,
                p.Unit,
                p.Price,
                p.PurchaseDate
            ))
            .ToListAsync(cancellationToken);

        var result = new GetPurchasesByDaysResponse(
            request.PageNumber,
            request.PageSize,
            totalCount,
            purchasesAfterSkip
        );

        return result;

    }
}
