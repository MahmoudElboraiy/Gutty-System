

using ErrorOr;
using MediatR;

namespace Application.Inventory.Purchases.Query.GetPriceSummaryByDays;

public record GetPriceSummaryByDaysQuery(int days) : IRequest<decimal>;

