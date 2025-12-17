
using MediatR;

namespace Application.Orders.Commands.GetOrCreateOrder;

public record CreateOrderCommand(int dayNumber):IRequest<CreateOrderCommandResponse>;
public record CreateOrderCommandResponse(int orderId, string Message);
