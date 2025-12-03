
using Application.Interfaces.UnitOfWorkInterfaces;
using ErrorOr;
using MediatR;

namespace Application.Inventory.Purchases.Query.GetPurchaseOrderById;

public class GetPurchaseOrderByIdQueryHandler : IRequestHandler<GetPurchaseOrderByIdQuery, ErrorOr<PurchaseOrderByIdResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    public GetPurchaseOrderByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<ErrorOr<PurchaseOrderByIdResponse>> Handle(GetPurchaseOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var purchase = await _unitOfWork.Purchases.GetByIdAsync(request.Id);
        if (purchase == null)
        {
            return Error.NotFound(description: "Purchase order not found.");
        }
        var response = new PurchaseOrderByIdResponse(
            purchase.Id,
            purchase.ItemName,
            purchase.Quantity,
            purchase.Unit,
            purchase.Price,
            purchase.PurchaseDate
        );
        return response;
    }
}
