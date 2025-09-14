using Application.Interfaces.UnitOfWorkInterfaces;
using Domain.DErrors;
using ErrorOr;
using MediatR;

namespace Application.Meals.Commands.UpdateMeal;

public class UpdateMealCommandHandler
    : IRequestHandler<UpdateMealCommand, ErrorOr<UpdateMealCommandResponse>>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateMealCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<UpdateMealCommandResponse>> Handle(
        UpdateMealCommand request,
        CancellationToken cancellationToken)
    {
        var meal = await _unitOfWork.Meals.GetByIdAsync(request.Id);
        if (meal == null)
        {
            return DomainErrors.Meals.MealNotFound(request.Id);
        }

        // Verify that the item exists if it's being changed
        if (meal.ItemId != request.ItemId)
        {
            var item = await _unitOfWork.Items.GetByIdAsync(request.ItemId);
            if (item == null)
            {
                return DomainErrors.Items.ItemNotFound(request.ItemId);
            }
        }

        meal.ItemId = request.ItemId;
        meal.Weight = request.Weight;
        meal.Price = request.Price;
        meal.MealType = request.MealType;
        meal.Quantity = request.Quantity;

        _unitOfWork.Meals.Update(meal);
        await _unitOfWork.CompleteAsync();

        return new UpdateMealCommandResponse(meal.Id);
    }
}