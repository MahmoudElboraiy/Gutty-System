

using Domain.Enums;
using ErrorOr;
using MediatR;

namespace Application.Inventory.Sales.Query.GetSaleOrderById;

public record GetSaleOrderByIdQuery(
    int Id
) : IRequest<ErrorOr<SaleOrderByIdResponse>>;
public record SaleOrderByIdResponse(
    int Id,
    string ItemName,
    SaleType ItemType,
    decimal Quantity,
    UnitType UnitType,
    decimal Price,
    string CustomerName,
    string CustomerPhone,
    DateOnly SaleDate
);
