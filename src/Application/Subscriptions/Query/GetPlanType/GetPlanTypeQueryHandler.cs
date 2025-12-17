
using Application.Interfaces;
using Application.Interfaces.UnitOfWorkInterfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Subscriptions.Query.GetPlanType;

public class GetPlanTypeQueryHandler : IRequestHandler<GetPlanTypeQuery, string>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;
    public GetPlanTypeQueryHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
    {
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
    }
    public async Task<string> Handle(GetPlanTypeQuery request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;
        var PlanName = await _unitOfWork.Subscriptions.GetQueryable()
            .Where(s => s.UserId == userId && s.IsCurrent)
            .Select(subscription => new
            {
                 subscription.Plan.Name
            })
            .FirstOrDefaultAsync(cancellationToken);
        if (PlanName == null)
            return "No Plan";
        return PlanName.Name;

    }
}
