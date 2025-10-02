
using MediatR;

namespace Application.Orders.Commands.ConfirmOrder;

public record ConfirmOrderCommand(int orderId) : IRequest<ConfirmOrderCommandRespnose>;
public record ConfirmOrderCommandRespnose(bool IsSuccess, string Message);
