

using Application.Interfaces.UnitOfWorkInterfaces;
using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Inventory.Purchases.Query.GetPurchasesByDays;

public class GetPurchasesByDaysQueryHandler : IRequestHandler<GetPurchasesByDaysQuery, ErrorOr<List<GetPurchasesByDaysResponse>>>
{
    private readonly IUnitOfWork _unitOfWork;
    public GetPurchasesByDaysQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<ErrorOr<List<GetPurchasesByDaysResponse>>> Handle(GetPurchasesByDaysQuery request, CancellationToken cancellationToken)
    {
        var cutoffDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-request.Days));
        var purchases = _unitOfWork.Purchases
            .GetQueryable() 
            .AsNoTracking()
            .Where(p => p.PurchaseDate >= cutoffDate) 
            .OrderByDescending(p => p.PurchaseDate)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(p => new GetPurchasesByDaysResponse
            {
                Id = p.Id,
                ItemName = p.ItemName,
                Quantity = p.Quantity,
                Unit = p.Unit,
                Price = p.Price,
                PurchaseDate = p.PurchaseDate
            });
        var result = await Task.FromResult(purchases.ToList());
        return result;
    }
}
