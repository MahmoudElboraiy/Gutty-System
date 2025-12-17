
using Application.Interfaces.UnitOfWorkInterfaces;
using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Inventory.Purchases.Query.GetPriceSummaryByDays;

public class GetPriceSummaryByDaysQueryHandler : IRequestHandler<GetPriceSummaryByDaysQuery, decimal>
{
    private readonly IUnitOfWork _unitOfWork;
    public GetPriceSummaryByDaysQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<decimal> Handle(GetPriceSummaryByDaysQuery request, CancellationToken cancellationToken)
    {
        var cutoffDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-request.days));
        var totalPrice = await _unitOfWork
                        .Purchases                       
                        .GetQueryable()
                        .AsNoTracking()
                        .Where(p => p.PurchaseDate >= cutoffDate)
                        .SumAsync(p => p.Price);
        return totalPrice;
    }
}
