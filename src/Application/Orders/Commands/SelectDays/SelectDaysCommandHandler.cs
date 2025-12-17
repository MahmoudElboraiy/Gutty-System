
using Application.Interfaces;
using Application.Interfaces.UnitOfWorkInterfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Orders.Commands.SelectDays;

public record SelectDaysCommandHandler : IRequestHandler<SelectDaysCommand, SelectDaysCommandResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;
    public SelectDaysCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
    {
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
    }
    public async Task<SelectDaysCommandResponse> Handle(SelectDaysCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;
        var subscription = await _unitOfWork.Subscriptions.GetQueryable()
            //.Include(p =>p.Plan)
            .Where(s => s.UserId == userId && s.IsCurrent && !s.IsPaused)
            .Select(s => new { s.Plan.BDMealsPerDay , s.Plan.LMealsPerDay, s.DaysLeft })
            .FirstOrDefaultAsync(cancellationToken);

        if (subscription == null)
        {
            return new SelectDaysCommandResponse(false, "No active subscription found.",0,0,0);
        }
        if (request.SelectedDays > subscription.DaysLeft)
        {
            return new SelectDaysCommandResponse(false, "Selected days exceed remaining days in subscription.", 0, 0, 0);
        }
        return new SelectDaysCommandResponse(true, "Days selected successfully.",request.SelectedDays,subscription.BDMealsPerDay *request.SelectedDays, subscription.LMealsPerDay * request.SelectedDays);
    }
}
