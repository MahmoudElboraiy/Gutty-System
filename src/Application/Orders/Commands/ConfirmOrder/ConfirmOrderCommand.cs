
using MediatR;

namespace Application.Orders.Commands.ConfirmOrder;

public record ConfirmOrderCommand() : IRequest<ConfirmOrderCommandRespnose>;
public record ConfirmOrderCommandRespnose(bool IsSuccess, string Message);
