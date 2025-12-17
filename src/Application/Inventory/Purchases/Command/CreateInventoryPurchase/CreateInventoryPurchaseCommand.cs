
using Domain.Enums;
using ErrorOr;
using MediatR;

namespace Application.Inventory.Purchases.Command.CreateInventoryPurchase;

public record CreateInventoryPurchaseCommand(string ItemName, int Quantity,UnitType Unit, decimal Price, DateOnly PurchaseDate):IRequest<ErrorOr<ResultMessage>>;
public record ResultMessage
{
    public bool IsSuccessed { get; set; }
    public string Message { get; set; }
}
