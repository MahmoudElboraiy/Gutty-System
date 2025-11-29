
using Domain.Enums;
using ErrorOr;
using MediatR;

namespace Application.Inventory.Purchases.Query.GetPurchasesByDays;

public record GetPurchasesByDaysQuery(int Days, int PageNumber, int PageSize) : IRequest<ErrorOr<List<GetPurchasesByDaysResponse>>>;
public record GetPurchasesByDaysResponse
{
    public int Id { get; set; }
    public string ItemName { get; set; }
    public int Quantity { get; set; }
    public UnitType Unit { get; set; }
    public decimal Price { get; set; }
    public DateOnly PurchaseDate { get; set; }
}

