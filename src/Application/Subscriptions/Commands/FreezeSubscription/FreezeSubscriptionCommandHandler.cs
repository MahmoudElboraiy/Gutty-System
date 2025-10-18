

using Application.Interfaces.UnitOfWorkInterfaces;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Subscriptions.Commands.FreezeSubscription;

public class FreezeSubscriptionCommandHandler : IRequestHandler<FreezeSubscriptionCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;
    public FreezeSubscriptionCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
    {
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
    }
    public async Task<bool> Handle(FreezeSubscriptionCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;
        var subscription = await _unitOfWork.Subscriptions.GetQueryable()
            .Where(s => s.UserId == userId && s.IsCurrent)
            .FirstOrDefaultAsync(cancellationToken);
        if (subscription == null)
        {
            return false;
        }
        subscription.IsPaused = !subscription.IsPaused;
        //_unitOfWork.Subscriptions.Update(subscription);
        await _unitOfWork.CompleteAsync();
        return subscription.IsPaused;
    }
}
