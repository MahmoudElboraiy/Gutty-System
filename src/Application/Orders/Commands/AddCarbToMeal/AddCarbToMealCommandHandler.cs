
using Application.Interfaces.UnitOfWorkInterfaces;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Orders.Commands.AddCarbToMeal;

public class AddCarbToMealCommandHandler : IRequestHandler<AddCarbToMealCommand, AddCarbToMealCommandResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    public AddCarbToMealCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<AddCarbToMealCommandResponse> Handle(AddCarbToMealCommand request, CancellationToken cancellationToken)
    {
        //var orderMeal2 = await _unitOfWork.OrderMeals.GetByIdAsync(request.orderMealId);
        var orderMeal = await _unitOfWork.OrderMeals.GetQueryable()
         .Where(om => om.Id == request.orderMealId)
         .Select(om => new
         {
             OrderMeal = om,
             ProteinMealId = om.ProteinMealId
         })
         .FirstOrDefaultAsync(cancellationToken);
        if (orderMeal == null)
        {
            return new AddCarbToMealCommandResponse(false, "Could not add Carb");
        }
        //var mealId = await _unitOfWork.OrderMeals.GetQueryable()
        //    .Where(om => om.Id == request.orderMealId)
        //    .Select(om => om.ProteinMealId)
        //    .FirstOrDefaultAsync(cancellationToken);

        if(orderMeal.ProteinMealId == null)
        {
            return new AddCarbToMealCommandResponse(false, "Could not find Meal");
        }
        var acceptCarb = await _unitOfWork.Meals.GetQueryable()
            .AsNoTracking()
            .Where(m => m.Id == orderMeal.ProteinMealId)
            .Select(m => m.AcceptCarb)
            .FirstOrDefaultAsync(cancellationToken);

        if (!acceptCarb)
        {
            return new AddCarbToMealCommandResponse(false, "This meal does not accept a carb");
        }
        var IsCarb =await _unitOfWork.Meals.GetQueryable()
            .AsNoTracking()
            .Where(m => m.Id == request.CarbId)
            .Select(m => (MealType?)m.MealType)
            .FirstOrDefaultAsync(cancellationToken);

        if(IsCarb != MealType.Carb)
        {
            return new AddCarbToMealCommandResponse(false, "This is not Carb");
        }
        orderMeal.OrderMeal.CarbMealId = request.CarbId;       
        _unitOfWork.OrderMeals.Update(orderMeal.OrderMeal);
        await _unitOfWork.CompleteAsync();
        return new AddCarbToMealCommandResponse(true , "The Carb had been added Successfully");
    }
}
