
using Application.Interfaces;
using Application.Interfaces.UnitOfWorkInterfaces;
using Application.Profiles;
using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Orders.Query.ShowOrderDetails;

public class ShowOrderDetailsQueryHandler : IRequestHandler<ShowOrderDetailsQuery, ErrorOr<ShowOrderDetailsQueryResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;
    public ShowOrderDetailsQueryHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
    {
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
    }
    public async Task<ErrorOr<ShowOrderDetailsQueryResponse>> Handle(ShowOrderDetailsQuery request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;
        var today = DateOnly.FromDateTime(DateTime.UtcNow);

        var orderId = await _unitOfWork.Orders.GetQueryable()
            .AsNoTracking()
            .Where(o => o.Subscription.UserId == userId && o.Subscription.IsCurrent && !o.Subscription.IsPaused &&
                        (!o.IsCompleted || o.DeliveryDate >= today))
            .Select(o => o.Id)
            .FirstOrDefaultAsync(cancellationToken);
        if(orderId == 0)
        {
            return Error.Validation("There is no order", "User does not have an active order.");
        }
        var orderMeals = await _unitOfWork.OrderMeals.GetQueryable()
            .AsNoTracking()
            .Where(om => om.OrderId == orderId)
            .Select(om => new
            {
                OrderMeal = om,
                Meals = new[]
                {
                    om.MealId,
                    om.ProteinMealId,
                    om.CarbMealId
                }
            })
            .ToListAsync(cancellationToken);

        var mealIds = orderMeals
           .SelectMany(om => om.Meals) // اجمع الثلاثة في List
           .Where(id => id.HasValue) // تجاهل null
           .Select(id => id.Value)   // رجع int مش nullable
           .ToList();

        if(mealIds.Count == 0)
        {
            return new ShowOrderDetailsQueryResponse(new List<ShowOrderMealsDetailsQueryResponseItem>());
        }
        var mealDetails =await _unitOfWork.Meals.GetQueryable()
            .AsNoTracking()
            .Where(m => mealIds.Contains(m.Id))
             .ToDictionaryAsync(
                m => m.Id,
                m => (m.Name, m.ImageUrl, m.AcceptCarb),
                cancellationToken
            );

        

        var result = new ShowOrderDetailsQueryResponse(
            orderMeals.Select(om => om.OrderMeal.MapOrderMealResponse(mealDetails)).ToList()
  );
        return result;
       
    }
}
