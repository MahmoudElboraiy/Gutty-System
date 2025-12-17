
using Application.Interfaces;
using Application.Interfaces.UnitOfWorkInterfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Subscriptions.Query.GetSubscriptionStatus;

public class GetSubscriptionStatusQueryHandler : IRequestHandler<GetSubscriptionStatusQuery, bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;
    public GetSubscriptionStatusQueryHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
    {
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
    }
    public async Task<bool> Handle(GetSubscriptionStatusQuery request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;
        var subscription = await _unitOfWork.Subscriptions.GetQueryable()
            .Where(s => s.UserId == userId && s.IsCurrent)
            .FirstOrDefaultAsync(cancellationToken);
        if (subscription == null)
        {
            return false;
        }
        return subscription.IsPaused;
    }
}
