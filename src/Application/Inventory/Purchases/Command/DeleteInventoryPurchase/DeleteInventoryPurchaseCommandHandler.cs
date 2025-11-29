
using Application.Interfaces.UnitOfWorkInterfaces;
using ErrorOr;
using MediatR;

namespace Application.Inventory.Purchases.Command.DeleteInventoryPurchase;

public class DeleteInventoryPurchaseCommandHandler : IRequestHandler<DeleteInventoryPurchaseCommand, ErrorOr<ResultMessage>>
{
    private readonly IUnitOfWork _unitOfWork;
    public DeleteInventoryPurchaseCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<ErrorOr<ResultMessage>> Handle(DeleteInventoryPurchaseCommand request, CancellationToken cancellationToken)
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
        _unitOfWork.Purchases.Remove(purchase);
        await _unitOfWork.CompleteAsync();
        return new ResultMessage
        {
            IsSuccessed = true,
            Message = "Purchase deleted successfully"
        };
    }
}
