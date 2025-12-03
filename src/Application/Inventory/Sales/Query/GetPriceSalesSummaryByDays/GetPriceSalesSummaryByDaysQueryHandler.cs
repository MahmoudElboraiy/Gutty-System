

using Application.Interfaces.UnitOfWorkInterfaces;
using ErrorOr;
using MediatR;

namespace Application.Inventory.Sales.Query.GetPriceSalesSummaryByDays;

public class GetPriceSalesSummaryByDaysQueryHandler : IRequestHandler<GetPriceSalesSummaryByDaysQuery, ErrorOr<List<PriceSalesSummaryResponse>>>
{
    private readonly IUnitOfWork _unitOfWork;
    public GetPriceSalesSummaryByDaysQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<ErrorOr<List<PriceSalesSummaryResponse>>> Handle(GetPriceSalesSummaryByDaysQuery request, CancellationToken cancellationToken)
    {
        var date = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-request.Days));

        var sales = await _unitOfWork.Sales.FindAsync(s => s.SaleDate >= date);

        if (sales is null || !sales.Any())
            return new List<PriceSalesSummaryResponse>();

        var total = sales.Sum(s => s.Price);

        var summaryByTypes = sales
            .GroupBy(s => s.ItemType)
            .Select(g => new PriceSalesSummary(
                g.Key,
                g.Sum(s => s.Price)
            ))
            .ToList();

        var response = new PriceSalesSummaryResponse(
            total,
            summaryByTypes
        );

        return new List<PriceSalesSummaryResponse> { response };
    }
}
