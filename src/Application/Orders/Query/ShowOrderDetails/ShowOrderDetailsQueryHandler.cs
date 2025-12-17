
using Application.Interfaces;
using Application.Interfaces.UnitOfWorkInterfaces;
using Application.Profiles;
using Domain.Enums;
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

        //var orderId = await _unitOfWork.Orders.GetQueryable()
        //    .AsNoTracking()
        //    .Where(o => o.Subscription.UserId == userId && o.Subscription.IsCurrent && !o.Subscription.IsPaused &&
        //                (!o.IsCompleted || o.DeliveryDate >= today))
        //    .Select(o => o.Id)
        //    .FirstOrDefaultAsync(cancellationToken);
        //if(orderId == 0)
        //{
        //    return Error.Validation("There is no order", "User does not have an active order.");
        //}

        var order = await _unitOfWork.Orders.GetQueryable()
           .AsNoTracking()
           .Include(o => o.Meals)
               .ThenInclude(om => om.Meal)
           .Include(o => o.Meals)
               .ThenInclude(om => om.ProteinMeal)
           .Include(o => o.Meals)
               .ThenInclude(om => om.CarbMeal)
           .Include(o => o.Subscription)
               .ThenInclude(s => s.Plan)
           .Where(o => o.Subscription.UserId == userId
                       && o.Subscription.IsCurrent
                       && !o.Subscription.IsPaused
                       && (!o.IsCompleted || o.DeliveryDate >= today))
           .FirstOrDefaultAsync(cancellationToken);

        if (order == null)
        {
            return Error.Validation(
                 code: "ORDER_NOT_FOUND",
                    description: "User Does not have Active Order"
                  );
        }
            var orderMeals = order.Meals.ToList();

        var mealDetails = orderMeals
            .SelectMany(om => new[] { om.Meal, om.ProteinMeal, om.CarbMeal })
            .Where(m => m != null)
            .DistinctBy(m => m!.Id)
            .ToDictionary(
                m => m!.Id,
                m => (m.Name, m.ImageUrl, m.AcceptCarb)
            );
        
        //var orderMeals = await _unitOfWork.OrderMeals.GetQueryable()
        //    .AsNoTracking()
        //    .Include(c =>c.CarbMeal)
        //    .Where(om => om.OrderId == orderId)
        //    .Select(om => new
        //    {
        //        OrderMeal = om,
        //        Meals = new[]
        //        {                  
        //            om.MealId,
        //            om.ProteinMealId,
        //            om.CarbMealId
        //        }
        //    })
        //    .ToListAsync(cancellationToken);

        //var mealIds = orderMeals
        //   .SelectMany(om => om.Meals) // اجمع الثلاثة في List
        //   .Where(id => id.HasValue) // تجاهل null
        //   .Select(id => id.Value)   // رجع int مش nullable
        //   .ToList();

        //if(mealIds.Count == 0)
        //{
        //    return new ShowOrderDetailsQueryResponse(new List<ShowOrderMealsDetailsQueryResponseItem>(),0,0,0,0);
        //}
        //var mealDetails =await _unitOfWork.Meals.GetQueryable()
        //    .AsNoTracking()
        //    .Where(m => mealIds.Contains(m.Id))
        //     .ToDictionaryAsync(
        //        m => m.Id,
        //        m => (m.Name, m.ImageUrl, m.AcceptCarb),
        //        cancellationToken
        //    );
         
        var dayNumber = order.DayNumber;

        var IsChangeDeleveryDate = order.DeliveryDate == null;

        //var dayNumber = _unitOfWork.Orders.GetQueryable()
        //    .AsNoTracking()
        //    .Where(o => o.Id == orderId)
        //    .Select(o=> o.DayNumber)
        //    .FirstOrDefault();

        var allowedBreakfastPerDay = order.Subscription.Plan.BDMealsPerDay;
        var allowedLunchPerDay = order.Subscription.Plan.LMealsPerDay;

        //var MealsPerDay =await _unitOfWork.Subscriptions.GetQueryable()
        //    .AsNoTracking()
        //    .Where(s => s.UserId == userId && s.IsCurrent && !s.IsPaused)
        //    .Select(s => new { s.Plan.BDMealsPerDay , s.Plan.LMealsPerDay})
        //    .FirstOrDefault(cancellationToken);

        var BDMealsSelected = order.Meals.Count(om => om.MealId.HasValue && om.MealType == MealType.BreakFastAndDinner);
        var LunchMealsSelected = order.Meals.Count(om => om.ProteinMealId.HasValue && om.MealType == MealType.Protien);


        //var LunchMealsSelected = _unitOfWork.OrderMeals.GetQueryable()
        //    .AsNoTracking()
        //    .Where(om => om.OrderId == orderId && om.ProteinMealId.HasValue)
        //    .Count();
        //var BDMealsSelected = _unitOfWork.OrderMeals.GetQueryable()
        //    .AsNoTracking()
        //    .Where(om => om.OrderId == orderId && om.MealId.HasValue)
        //    .Count();


        var result = new ShowOrderDetailsQueryResponse(
              order.Meals.Select(om => om.MapOrderMealResponse(mealDetails)).ToList(),
            // orderMeals.Select(om => om.OrderMeal.MapOrderMealResponse(mealDetails2)).ToList(),
            BDMealsSelected,
            (int)allowedBreakfastPerDay*dayNumber,
            LunchMealsSelected,
            (int)allowedLunchPerDay*dayNumber,
            IsChangeDeleveryDate
          );
        return result;
       
    }
}
