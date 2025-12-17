

using Application.Interfaces.UnitOfWorkInterfaces;
using Domain.Models.Identity;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Inventory.Sales.Command.CreateInventorySales;

public class CreateInventorySalesCommandHandler: IRequestHandler<CreateInventorySalesCommand, ErrorOr<ResultMessage>>
{
    private readonly IUnitOfWork _unitOfWork;
    public UserManager<User> _UserManager { get; }

    public CreateInventorySalesCommandHandler(IUnitOfWork unitOfWork,UserManager<User> userManager)
    {
        _unitOfWork = unitOfWork;
        _UserManager = userManager;
    }
    public async Task<ErrorOr<ResultMessage>> Handle(CreateInventorySalesCommand request, CancellationToken cancellationToken)
    {
        var Customer = await _UserManager.FindByNameAsync(request.CustomerPhoneNumber);
        if (Customer == null) {
            return new ResultMessage
            {
                IsSuccessed = false,
                Message = "Customer not found."
            };
        }
        var CustomerId = Customer?.Id;
        var sale = new Domain.Models.Entities.Sales
        {
            ItemName = request.ItemName,
            ItemType = request.ItemType,
            Quantity = request.Quantity,
            UnitType = request.UnitType,
            Price = request.Price,
            CustomerId = CustomerId,
            SaleDate = request.SaleDate
        };
        await _unitOfWork.Sales.AddAsync(sale);
        await _unitOfWork.CompleteAsync();
        return new ResultMessage
        {
            IsSuccessed = true,
            Message = "Inventory sale created successfully."
        };
    }
}
