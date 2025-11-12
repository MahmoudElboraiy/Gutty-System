

using ErrorOr;
using MediatR;

namespace Application.Plans.Commands.DeletePlan;

public record DeletePlanCommand(Guid id):IRequest<ErrorOr<ResultMessage>>;
public record ResultMessage(
    bool IsSuccess,
    string Message
    );

