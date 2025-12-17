
using Application.Interfaces.UnitOfWorkInterfaces;
using ErrorOr;
using MediatR;

namespace Application.Inventory.Purchases.Command.CreateInventoryPurchase;

public class CreateInventoryPurchaseCommandHandler : IRequestHandler<CreateInventoryPurchaseCommand, ErrorOr<ResultMessage>>
{
    private readonly IUnitOfWork _unitOfWork;
    public CreateInventoryPurchaseCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<ErrorOr<ResultMessage>> Handle(CreateInventoryPurchaseCommand request, CancellationToken cancellationToken)
    {
        var purchase = new Domain.Models.Entities.Purchases
        {
            ItemName = request.ItemName,
            Quantity = request.Quantity,
            Unit = request.Unit,
            Price = request.Price,
            PurchaseDate = request.PurchaseDate
        };
        await _unitOfWork.Purchases.AddAsync(purchase);
        await _unitOfWork.CompleteAsync();
        return new ResultMessage
        {
            IsSuccessed = true,
            Message = "Purchase created successfully"
        };
    }

}
