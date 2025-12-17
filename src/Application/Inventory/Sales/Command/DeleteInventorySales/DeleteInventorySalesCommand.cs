

using ErrorOr;
using MediatR;

namespace Application.Inventory.Sales.Command.DeleteInventorySales;

public record DeleteInventorySalesCommand(int Id) : IRequest<ErrorOr<ResultMessage>>;
public record ResultMessage
{
    public bool IsSuccessed { get; set; }
    public string Message { get; set; }
}
