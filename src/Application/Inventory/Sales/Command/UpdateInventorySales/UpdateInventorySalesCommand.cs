

using Domain.Enums;
using ErrorOr;
using MediatR;

namespace Application.Inventory.Sales.Command.UpdateInventorySales;

public record UpdateInventorySalesCommand(
    int Id,
    string ItemName,
    SaleType ItemType,
    decimal Quantity,
    UnitType UnitType,
    decimal Price,
    string CustomerPhoneNumber,
    DateOnly SaleDate
) : IRequest<ErrorOr<ResultMessage>>;
public record ResultMessage
{
    public bool IsSuccessed { get; set; }
    public string Message { get; set; }
}
