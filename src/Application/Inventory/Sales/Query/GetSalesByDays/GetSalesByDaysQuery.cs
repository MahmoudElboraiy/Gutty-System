

using Domain.Enums;
using ErrorOr;
using MediatR;

namespace Application.Inventory.Sales.Query.GetSalesByDays;

public record GetSalesByDaysQuery(
    int Days,
    int PageNumber,
    int PageSize,
    string? searchName
) : IRequest<ErrorOr<GetSalesByDaysQueryResponse>>;
public record GetSalesByDaysQueryResponse(
    int pageNumber,
    int pageSize,
    int TotalCount,
    List<GetSalesByDaysItem> Sales );
public record class GetSalesByDaysItem(
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

