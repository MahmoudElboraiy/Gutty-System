

using Application.Interfaces.UnitOfWorkInterfaces;
using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Inventory.Sales.Query.GetSalesByDays;

public class GetSalesByDaysQueryHandler:IRequestHandler<GetSalesByDaysQuery,ErrorOr<GetSalesByDaysQueryResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    public GetSalesByDaysQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<ErrorOr<GetSalesByDaysQueryResponse>> Handle(GetSalesByDaysQuery request, CancellationToken cancellationToken)
    {
        var cutoffDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-request.Days));

        // 1) Base query
        var query = _unitOfWork.Sales
            .GetQueryable()
            .AsNoTracking()
            .Include(s => s.Customer)
            .Where(s => s.SaleDate >= cutoffDate);

        // 2) Apply search if provided (case-insensitive)
        if (!string.IsNullOrWhiteSpace(request.searchName))
        {
            var search = request.searchName.ToLower();
            query = query.Where(s =>
                s.ItemName.ToLower().Contains(search)
            );
        }

        int totalCount = await query.CountAsync(cancellationToken);

        int skip = (request.PageNumber - 1) * request.PageSize;

        var sales = await query
            .OrderByDescending(s => s.SaleDate)
            .Skip(skip)
            .Take(request.PageSize)
            .Select(s => new GetSalesByDaysItem(
                s.Id,
                s.ItemName,
                s.ItemType,
                s.Quantity,
                s.UnitType,
                s.Price,
                s.Customer.Name,
                s.Customer.PhoneNumber,
                s.SaleDate
            ))
            .ToListAsync(cancellationToken);

        var finalResponse = new GetSalesByDaysQueryResponse(
            request.PageNumber,
            request.PageSize,
            totalCount,
            sales
        );

        return finalResponse;
    }
}
