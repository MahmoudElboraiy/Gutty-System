

using Domain.Enums;
using ErrorOr;
using MediatR;

namespace Application.Inventory.Sales.Query.GetPriceSalesSummaryByDays;

public record GetPriceSalesSummaryByDaysQuery(int Days):IRequest<ErrorOr<List<PriceSalesSummaryResponse>>>;
public record PriceSalesSummaryResponse(decimal total  ,List<PriceSalesSummary> PriceSalesSummaries);
public record PriceSalesSummary(SaleType SaleType, decimal TotalSalesPrice);
