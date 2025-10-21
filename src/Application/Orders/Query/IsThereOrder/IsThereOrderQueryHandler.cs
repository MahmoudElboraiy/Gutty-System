

using Application.Interfaces;
using Application.Interfaces.UnitOfWorkInterfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Orders.Query.IsThereOrder
{
    public class IsThereOrderQueryHandler : IRequestHandler<IsThereOrderQuery, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;
        public IsThereOrderQueryHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
        }
        public async Task<bool> Handle(IsThereOrderQuery request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;
            var today = DateOnly.FromDateTime(DateTime.UtcNow);

            var hasActiveOrder = await _unitOfWork.Orders.GetQueryable()
                .AnyAsync(o => o.Subscription.UserId == userId &&
                               o.Subscription.IsCurrent &&
                               !o.Subscription.IsPaused &&
                               (!o.IsCompleted || o.DeliveryDate >= today),
                           cancellationToken);
            return hasActiveOrder;
        }
    }
}
