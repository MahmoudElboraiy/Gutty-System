
using MediatR;

namespace Application.Orders.Commands.FreezeAccount.FreezeSubscription;

public record FreezeSubscriptionCommand : IRequest<bool>;
