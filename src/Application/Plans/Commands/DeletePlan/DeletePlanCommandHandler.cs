

using Application.Interfaces.UnitOfWorkInterfaces;
using ErrorOr;
using MediatR;

namespace Application.Plans.Commands.DeletePlan;

public class DeletePlanCommandHandler: IRequestHandler<DeletePlanCommand,ErrorOr<ResultMessage>>
{
    private readonly IUnitOfWork _unitOfWork;
    public DeletePlanCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<ErrorOr<ResultMessage>> Handle (DeletePlanCommand request,CancellationToken cancellationToken)
    {
        var planExits = await _unitOfWork.Plans.GetByIdAsync(request.id);
        if (planExits == null)
        {
            return Error.NotFound(description: "Plan not found");
        }
        _unitOfWork.Plans.Remove(planExits);
        await _unitOfWork.CompleteAsync();
        return new ResultMessage(true, "Plan deleted successfully");
    }
}
