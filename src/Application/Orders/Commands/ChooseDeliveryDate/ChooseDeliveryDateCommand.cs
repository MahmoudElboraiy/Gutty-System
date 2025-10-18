
using MediatR;

namespace Application.Orders.Commands.ChooseDeliveryDate;

public record ChooseDeliveryDateCommand(DateOnly deliveryDate) : IRequest<ChooseDeliveryDateCommandResponse>;
public record ChooseDeliveryDateCommandResponse( bool Success, string Message );