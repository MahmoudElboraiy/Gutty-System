
using MediatR;

namespace Application.Subscriptions.Query.GetSubscriptionStatus;

public record GetSubscriptionStatusQuery() : IRequest<bool>;
