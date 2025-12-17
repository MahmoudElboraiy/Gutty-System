
using Application.Interfaces.UnitOfWorkInterfaces;
using ErrorOr;
using MediatR;

namespace Application.Inventory.Sales.Command.DeleteInventorySales;

public class DeleteInventorySalesCommandHandler : IRequestHandler<DeleteInventorySalesCommand, ErrorOr<ResultMessage>>
{
    private readonly IUnitOfWork _unitOfWork;
    public DeleteInventorySalesCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<ErrorOr<ResultMessage>> Handle(DeleteInventorySalesCommand request, CancellationToken cancellationToken)
    {
        var sale = await _unitOfWork.Sales.GetByIdAsync(request.Id);
        if (sale == null)
        {
            return new ResultMessage
            {
                IsSuccessed = false,
                Message = "The specified sale was not found."
            };
        }
        _unitOfWork.Sales.Remove(sale);
        await _unitOfWork.CompleteAsync();
        return new ResultMessage
        {
            IsSuccessed = true,
            Message = "Sale deleted successfully"
        };
    }
}
