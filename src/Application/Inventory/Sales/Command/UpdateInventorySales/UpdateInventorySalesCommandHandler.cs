

using Application.Interfaces.UnitOfWorkInterfaces;
using Domain.Models.Identity;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Inventory.Sales.Command.UpdateInventorySales;

public class UpdateInventorySalesCommandHandler : IRequestHandler<UpdateInventorySalesCommand, ErrorOr<ResultMessage>>
{
    private readonly IUnitOfWork _unitOfWork;
    public UserManager<User> _UserManager { get; }
    public UpdateInventorySalesCommandHandler(IUnitOfWork unitOfWork, UserManager<User> userManager)
    {
        _unitOfWork = unitOfWork;
        _UserManager = userManager;
    }
    public async Task<ErrorOr<ResultMessage>> Handle(UpdateInventorySalesCommand request, CancellationToken cancellationToken)
    {
        var sale = await _unitOfWork.Sales.GetByIdAsync(request.Id);
        if (sale == null)
        {
            return new ResultMessage
            {
                IsSuccessed = false,
                Message = "This Item sale not found."
            };
        }
        var Customer = await _UserManager.FindByNameAsync(request.CustomerPhoneNumber);
        if (Customer == null)
        {
            return new ResultMessage
            {
                IsSuccessed = false,
                Message = "Customer not found."
            };
        }
        var CustomerId = Customer?.Id;
        sale.ItemName = request.ItemName;
        sale.ItemType = request.ItemType;
        sale.Quantity = request.Quantity;
        sale.UnitType = request.UnitType;
        sale.Price = request.Price;
        sale.CustomerId = CustomerId;
        sale.SaleDate = request.SaleDate;
        _unitOfWork.Sales.Update(sale);
        await _unitOfWork.CompleteAsync();
        return new ResultMessage
        {
            IsSuccessed = true,
            Message = "Sale updated successfully"
        };
    }
}
