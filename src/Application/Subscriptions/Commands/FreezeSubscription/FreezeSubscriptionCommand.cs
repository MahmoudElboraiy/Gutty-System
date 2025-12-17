
using MediatR;

namespace Application.Subscriptions.Commands.FreezeSubscription;

public record FreezeSubscriptionCommand : IRequest<bool>;