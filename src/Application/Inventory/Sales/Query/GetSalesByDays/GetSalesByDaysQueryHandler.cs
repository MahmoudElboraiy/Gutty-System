

using Application.Interfaces.UnitOfWorkInterfaces;
using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Inventory.Sales.Query.GetSalesByDays;

public class GetSalesByDaysQueryHandler:IRequestHandler<GetSalesByDaysQuery,ErrorOr<List<GetSalesByDaysQueryResponse>>>
{
    private readonly IUnitOfWork _unitOfWork;
    public GetSalesByDaysQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<ErrorOr<List<GetSalesByDaysQueryResponse>>> Handle(GetSalesByDaysQuery request, CancellationToken cancellationToken)
    {
        var cutoffDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-request.Days));
        var sales = await _unitOfWork.Sales
            .GetQueryable()
            .AsNoTracking()
            .Include(s => s.Customer)
            .Where(s => s.SaleDate >= cutoffDate)
            .OrderByDescending(s => s.SaleDate)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var response = sales.Select(s => new GetSalesByDaysQueryResponse(
            s.Id,
            s.ItemName,
            s.ItemType,
            s.Quantity,
            s.UnitType,
            s.Price,
            s.Customer.Name,
            s.Customer.PhoneNumber,
            s.SaleDate
        )).ToList();
        return response;
    }
}
