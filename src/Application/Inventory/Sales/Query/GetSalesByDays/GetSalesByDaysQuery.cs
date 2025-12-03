

using Domain.Enums;
using ErrorOr;
using MediatR;

namespace Application.Inventory.Sales.Query.GetSalesByDays;

public record GetSalesByDaysQuery(
    int Days,
    int PageNumber,
    int PageSize
) : IRequest<ErrorOr<List<GetSalesByDaysQueryResponse>>>;
public record GetSalesByDaysQueryResponse(
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

