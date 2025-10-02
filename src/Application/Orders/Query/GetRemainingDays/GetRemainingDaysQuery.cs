

using MediatR;

namespace Application.Orders.Query.GetRemainingDays;

public record GetRemainingDaysQuery : IRequest<GetRemainingDaysQueryResponse>;
public record GetRemainingDaysQueryResponse(uint RemainDays);
