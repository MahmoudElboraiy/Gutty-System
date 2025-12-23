
using Application.Interfaces.UnitOfWorkInterfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Orders.Commands.RemoveCarbFromMeal;

public class RemoveCarbFromMealCommandHandler : IRequestHandler<RemoveCarbFromMealCommand, RemoveCarbFromMealCommandResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    public RemoveCarbFromMealCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<RemoveCarbFromMealCommandResponse> Handle(RemoveCarbFromMealCommand request, CancellationToken cancellationToken)
    {
        var orderMeal = await _unitOfWork.OrderMeals
              .GetQueryable()                      
              .FirstOrDefaultAsync(o => o.Id == request.orderMealId);
   
        if (orderMeal == null)
        {
            return new RemoveCarbFromMealCommandResponse(false, "Could not remove Carb");
        }
        orderMeal.CarbMealId = null;
       // _unitOfWork.OrderMeals.Update(orderMeal);
        await _unitOfWork.CompleteAsync();
        return new RemoveCarbFromMealCommandResponse(true, "The Carb had been removed Successfully");
    }
}
