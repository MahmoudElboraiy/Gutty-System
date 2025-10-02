
using Application.Interfaces.UnitOfWorkInterfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Orders.Commands.ChooseDeliveryDate;

public class ChooseDeliveryDateCommandHandler : IRequestHandler<ChooseDeliveryDateCommand, ChooseDeliveryDateCommandResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISystemConfigurationRepository _systemConfigurationRepository;
    public ChooseDeliveryDateCommandHandler(IUnitOfWork unitOfWork, ISystemConfigurationRepository systemConfigurationRepository)
    {
        _unitOfWork = unitOfWork;
        _systemConfigurationRepository = systemConfigurationRepository;
    }
    public async Task<ChooseDeliveryDateCommandResponse> Handle(ChooseDeliveryDateCommand request, CancellationToken cancellationToken)
    {
        var order = await _unitOfWork.Orders.GetByIdAsync(request.orderId);
        var config = await _systemConfigurationRepository.GetAsync(cancellationToken);
        if (order == null)
        {
            return new ChooseDeliveryDateCommandResponse(false, "Order not found");
        }
        var TodayDate = DateOnly.FromDateTime(DateTime.UtcNow);
        if (request.deliveryDate <= TodayDate.AddDays(config.MinimumDaysToOrder ?? 0))
        {
            return new ChooseDeliveryDateCommandResponse(false, $"Delivery date must be at least {config.MinimumDaysToOrder} days from today");
        }
        var count = await _unitOfWork.Orders
            .GetQueryable()
            .AsNoTracking()
            .Where(o => o.DeliveryDate.HasValue && o.DeliveryDate.Value == request.deliveryDate)
            .Select(o => o.Id)
            .CountAsync(cancellationToken);

        if (count >= (config.DailyCapacity ?? 0))
        {
            return new ChooseDeliveryDateCommandResponse(false, "Selected delivery date is fully booked. Please choose another date.");
        }
        order.DeliveryDate = request.deliveryDate;
        //_unitOfWork.Orders.Update(order);
        await _unitOfWork.CompleteAsync();
        return new ChooseDeliveryDateCommandResponse(true, "Delivery date chosen successfully.");
    }
}
