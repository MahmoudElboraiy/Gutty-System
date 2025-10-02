
using Application.Interfaces.UnitOfWorkInterfaces;
using Application.Profiles;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Orders.Query.ShowOrderDetails;

public class ShowOrderDetailsQueryHandler : IRequestHandler<ShowOrderDetailsQuery, ShowOrderDetailsQueryResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    public ShowOrderDetailsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<ShowOrderDetailsQueryResponse> Handle(ShowOrderDetailsQuery request, CancellationToken cancellationToken)
    {
        var orderMeals = await _unitOfWork.OrderMeals.GetQueryable()
            .AsNoTracking()
            .Where(om => om.OrderId == request.OrderId)
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
