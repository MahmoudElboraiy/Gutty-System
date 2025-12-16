

using Application.Interfaces.UnitOfWorkInterfaces;
using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
        bool isUsed = await _unitOfWork.Subscriptions
          .GetQueryable()
          .AsNoTracking()
          .AnyAsync(s => s.PlanId == planExits.Id && s.IsCurrent, cancellationToken);
        if (isUsed)
        {
            return Error.Validation(
                code: "Plan.DeleteError",
                description: $"Cannot delete plan with id {request.id} because it is associated with active subscriptions."
                );
        }
        _unitOfWork.Plans.Remove(planExits);
        await _unitOfWork.CompleteAsync();
        return new ResultMessage(true, "Plan deleted successfully");
    }
}
