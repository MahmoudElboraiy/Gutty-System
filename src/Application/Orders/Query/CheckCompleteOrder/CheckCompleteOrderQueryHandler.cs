
using Application.Interfaces.UnitOfWorkInterfaces;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Orders.Query.CheckCompleteOrder
{
    public class CheckCompleteOrderQueryHandler : IRequestHandler<CheckCompleteOrderQuery, CheckCompleteOrderQueryResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CheckCompleteOrderQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<CheckCompleteOrderQueryResponse> Handle(CheckCompleteOrderQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.Orders.GetQueryable()
                 .Where(o => o.Id == request.OrderId)
                 .Select(o => new
                 {
                    o.Id,
                    o.DayNumber,
                    Plan = new
                 {
                    o.Subscription.Plan.BDMealsPerDay,
                    o.Subscription.Plan.LMealsPerDay
                 },
                     MealCounts = o.Meals
                    .GroupBy(m => m.MealType)
                    .Select(g => new { MealType = g.Key, Count = g.Count() })
                    
       })
       .FirstOrDefaultAsync(cancellationToken);


            if (result == null)
            {
                return new CheckCompleteOrderQueryResponse(false, "Order not found.");
            }
            if(result.Plan == null)
            {
                return new CheckCompleteOrderQueryResponse(false, "Plan not found for the subscription.");
            }
            int BDMealsCount = result.MealCounts
                .FirstOrDefault(mc => mc.MealType == MealType.BreakFastAndDinner)?.Count ?? 0;

            int LMealsCount = result.MealCounts
                .FirstOrDefault(mc => mc.MealType == MealType.Protien)?.Count ?? 0;


            if (BDMealsCount ==result.DayNumber *result.Plan.BDMealsPerDay &&
                LMealsCount == result.DayNumber * result.Plan.LMealsPerDay) 
                return new CheckCompleteOrderQueryResponse(true, "Order is complete.");
            else
                return new CheckCompleteOrderQueryResponse(false, "Order is not complete.");
        }
    }
}
