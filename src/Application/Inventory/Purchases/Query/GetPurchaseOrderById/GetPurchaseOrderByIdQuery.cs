

using Domain.Enums;
using ErrorOr;
using MediatR;

namespace Application.Inventory.Purchases.Query.GetPurchaseOrderById;

public record GetPurchaseOrderByIdQuery(
    int Id
) : IRequest<ErrorOr<PurchaseOrderByIdResponse>>;
public record PurchaseOrderByIdResponse(
    int Id,
    string ItemName,
    decimal Quantity,
    UnitType UnitType,
    decimal Price,
    DateOnly PurchaseDate
);
