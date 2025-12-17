
using MediatR;

namespace Application.Orders.Query.IsThereOrder;

public record IsThereOrderQuery: IRequest<bool>;
