
using MediatR;

namespace Application.Subscriptions.Query.GetPlanType;

public record GetPlanTypeQuery() : IRequest<string>;
