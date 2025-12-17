
using Domain.Enums;
using ErrorOr;
using MediatR;

namespace Application.Inventory.Purchases.Query.GetPurchasesByDays;

public record GetPurchasesByDaysQuery(int Days, int PageNumber, int PageSize,string? searchName) : IRequest<ErrorOr<GetPurchasesByDaysResponse>>;
public record GetPurchasesByDaysResponse
(
    int pageNumber,
    int pageSize,
    int TotalCount,
    List<GetPurchasesByDaysItem> Purchases);
public record  GetPurchasesByDaysItem
(
    int Id,
    string ItemName,
    int Quantity,
    UnitType Unit,
    decimal Price ,
    DateOnly PurchaseDate
);

