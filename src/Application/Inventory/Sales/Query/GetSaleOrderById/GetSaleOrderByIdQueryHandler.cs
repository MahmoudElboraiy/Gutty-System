

using Application.Interfaces.UnitOfWorkInterfaces;
using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Inventory.Sales.Query.GetSaleOrderById;

public class GetSaleOrderByIdQueryHandler : IRequestHandler<GetSaleOrderByIdQuery, ErrorOr<SaleOrderByIdResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    public GetSaleOrderByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<ErrorOr<SaleOrderByIdResponse>> Handle(GetSaleOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var sale = await _unitOfWork.Sales.GetQueryable()
            .Include(s => s.Customer)
            .AsNoTracking()
            .Where(s=>s.Id==request.Id)
            .FirstOrDefaultAsync();
        if (sale == null)
        {
            return Error.NotFound(description: "Sale order not found.");
        }
        var response = new SaleOrderByIdResponse(
            sale.Id,
            sale.ItemName,
            sale.ItemType,
            sale.Quantity,
            sale.UnitType,
            sale.Price,
            sale.Customer.Name,
            sale.Customer.PhoneNumber,
            sale.SaleDate
        );
        return response;
    }
}
