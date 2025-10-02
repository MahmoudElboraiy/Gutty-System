
using MediatR;

namespace Application.Orders.Query.CheckCompleteOrder;

public record CheckCompleteOrderQuery(int OrderId) : IRequest<CheckCompleteOrderQueryResponse>;
public record CheckCompleteOrderQueryResponse(bool IsComplete,string Message);