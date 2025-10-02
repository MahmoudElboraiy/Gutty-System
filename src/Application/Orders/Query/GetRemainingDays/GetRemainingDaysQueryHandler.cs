

using Application.Interfaces;
using Application.Interfaces.UnitOfWorkInterfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;

namespace Application.Orders.Query.GetRemainingDays;

public class GetRemainingDaysQueryHandler : IRequestHandler<GetRemainingDaysQuery, GetRemainingDaysQueryResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;
    public GetRemainingDaysQueryHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
    {
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
    }
    public async Task<GetRemainingDaysQueryResponse> Handle(GetRemainingDaysQuery request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;
        var subscription = await _unitOfWork.Subscriptions.GetQueryable()
            .Where(s => s.UserId == userId && s.IsCurrent && !s.IsPaused)
            .Select(s => new { s.DaysLeft })
            .FirstOrDefaultAsync(cancellationToken);
        if (subscription == null)
        {
            return new GetRemainingDaysQueryResponse(0);
        }
        else
        {
            return new GetRemainingDaysQueryResponse(subscription.DaysLeft);
        }
    }
}
