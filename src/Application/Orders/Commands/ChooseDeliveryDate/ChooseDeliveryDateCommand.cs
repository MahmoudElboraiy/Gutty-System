
using MediatR;

namespace Application.Orders.Commands.ChooseDeliveryDate;

public record ChooseDeliveryDateCommand(int orderId, DateOnly deliveryDate) : IRequest<ChooseDeliveryDateCommandResponse>;
public record ChooseDeliveryDateCommandResponse( bool Success, string Message );