
using MediatR;

namespace Application.Orders.Query.CheckCompleteOrder;

public record CheckCompleteOrderQuery() : IRequest<CheckCompleteOrderQueryResponse>;
public record CheckCompleteOrderQueryResponse(bool IsComplete,string Message);