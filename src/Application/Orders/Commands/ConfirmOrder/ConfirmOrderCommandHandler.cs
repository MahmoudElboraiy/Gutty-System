
using Application.Interfaces;
using Application.Interfaces.UnitOfWorkInterfaces;
using Domain.Models.Identity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Orders.Commands.ConfirmOrder;

public class ConfirmOrderCommandHandler : IRequestHandler<ConfirmOrderCommand, ConfirmOrderCommandRespnose>
{
    
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;
    public ConfirmOrderCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
    {
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
    }
    public async Task<ConfirmOrderCommandRespnose> Handle(ConfirmOrderCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;

        var order = await _unitOfWork.Orders.GetQueryable()
         .AsNoTracking()
          .Where(o =>
            o.Subscription.UserId == userId &&
            o.Subscription.IsCurrent &&
            !o.Subscription.IsPaused &&
            !o.IsCompleted)
         .FirstOrDefaultAsync(cancellationToken);

        if (order == null)
        {
            return new ConfirmOrderCommandRespnose(false, "Order not found.");
        }
        if (order.IsCompleted)
        {
            return new ConfirmOrderCommandRespnose(false, "Order is already Compeleted.");
        }
       
        if(order.Subscription == null || order.Subscription.DaysLeft < order.DayNumber)
        {
            return new ConfirmOrderCommandRespnose(false, "Not enough days left in the subscription.");
        }

        order.IsCompleted = true;

        order.Subscription.DaysLeft -= (uint)order.DayNumber;
        if (order.Subscription.DaysLeft==0)
        {
            order.Subscription.IsCurrent = false;
        }
        await _unitOfWork.CompleteAsync();
        return new ConfirmOrderCommandRespnose(true, "Order confirmed successfully.");
    }
}
