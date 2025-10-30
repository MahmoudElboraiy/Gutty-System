
using Application.Interfaces.UnitOfWorkInterfaces;
using Application.Interfaces;
using MediatR;
using Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Orders.Commands.GetOrCreateOrder;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, CreateOrderCommandResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;
    public CreateOrderCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
    {
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
    }
    public async Task<CreateOrderCommandResponse> Handle(CreateOrderCommand request ,CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;
        var subscription = await _unitOfWork.Subscriptions.GetQueryable()
            .Where(s => s.UserId == userId && s.IsCurrent && !s.IsPaused)
            .Select(s => new { s.Id ,s.DaysLeft})
            .FirstOrDefaultAsync(cancellationToken);

        if(subscription == null)
        {
            return new CreateOrderCommandResponse(0, "No active subscription found for the user.");
        }

        var order = await _unitOfWork.Orders
            .GetQueryable()
            .FirstOrDefaultAsync(
                o => o.SubscriptionId == subscription.Id && !o.IsCompleted,
                cancellationToken);

        if(order != null)
        {
            return new CreateOrderCommandResponse(-1, "There is already an existing order");
        }

        if(subscription.DaysLeft< request.dayNumber)
        {
            return new CreateOrderCommandResponse(0, "Not enough days left in the subscription");
        }

        if (order == null)
        {      
            var lastDelivery = await _unitOfWork.Orders
                .GetQueryable()
                .Where(o => o.SubscriptionId == subscription.Id)
                .Select(subscription => subscription.DeliveryDate)
                .DefaultIfEmpty()
                .MaxAsync(cancellationToken);

            if (lastDelivery.HasValue && lastDelivery >= DateOnly.FromDateTime(DateTime.UtcNow))
            {
                return new CreateOrderCommandResponse(0, "The old order does not arriave yet");
            }
            order = new Order
            {
                SubscriptionId = subscription.Id,
                OrderDate = DateOnly.FromDateTime(DateTime.UtcNow),
                DayNumber = request.dayNumber,
                IsCompleted = false
            };

            await _unitOfWork.Orders.AddAsync(order);
            await _unitOfWork.CompleteAsync();
        }
        return new CreateOrderCommandResponse(order.Id, "This is you Current Order");
    }

}
