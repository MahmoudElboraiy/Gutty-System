

using ErrorOr;
using MediatR;

namespace Application.Inventory.Purchases.Command.UpdateInventoryPurchase;

public record UpdateInventoryPurchaseCommand(int Id, string ItemName, int Quantity, Domain.Enums.UnitType Unit, decimal Price, DateOnly PurchaseDate) : IRequest<ErrorOr<ResultMessage>>;
public record ResultMessage
{
    public bool IsSuccessed { get; set; }
    public string Message { get; set; }
}