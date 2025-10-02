
using MediatR;

namespace Application.Orders.Commands.GetOrCreateOrder;

public record GetOrCreateOrderCommand(int dayNumber):IRequest<GetOrCreateOrderCommandResponse>;
public record GetOrCreateOrderCommandResponse(int orderId, string Message);
