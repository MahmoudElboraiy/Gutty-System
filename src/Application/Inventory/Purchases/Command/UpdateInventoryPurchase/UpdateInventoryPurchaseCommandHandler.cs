

using Application.Interfaces.UnitOfWorkInterfaces;
using ErrorOr;
using MediatR;

namespace Application.Inventory.Purchases.Command.UpdateInventoryPurchase;

public class UpdateInventoryPurchaseCommandHandler : IRequestHandler<UpdateInventoryPurchaseCommand, ErrorOr<ResultMessage>>
{
    private readonly IUnitOfWork _unitOfWork;
    public UpdateInventoryPurchaseCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<ErrorOr<ResultMessage>> Handle(UpdateInventoryPurchaseCommand request, CancellationToken cancellationToken)
    {
        var purchase = await _unitOfWork.Purchases.GetByIdAsync(request.Id);
        if (purchase == null)
        {
            return new ResultMessage
            {
                IsSuccessed = false,
                Message = "The specified purchase was not found."
            };           
        }
        purchase.ItemName = request.ItemName;
        purchase.Quantity = request.Quantity;
        purchase.Unit = request.Unit;
        purchase.Price = request.Price;
        purchase.PurchaseDate = request.PurchaseDate;
        _unitOfWork.Purchases.Update(purchase);
        await _unitOfWork.CompleteAsync();
        return new ResultMessage
        {
            IsSuccessed = true,
            Message = "Purchase updated successfully"
        };
    }
}
