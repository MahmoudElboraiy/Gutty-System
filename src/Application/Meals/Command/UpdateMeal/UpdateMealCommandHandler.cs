

using Application.Interfaces.UnitOfWorkInterfaces;
using ErrorOr;
using MediatR;

namespace Application.Meals.Command.UpdateMeal;

public class UpdateMealCommandHandler : IRequestHandler<UpdateMealCommand, ErrorOr<ResultMessage>>
{
    private readonly IUnitOfWork _unitOfWork;
    public UpdateMealCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<ErrorOr<ResultMessage>> Handle(UpdateMealCommand request, CancellationToken cancellationToken)
    {
        var meal = await _unitOfWork.Meals.GetByIdAsync(request.Id);
        if (meal == null)
        {
            return Error.NotFound("Meal.NotFound", "Meal not found");
        }
        meal.Name = request.Name;
        meal.ImageUrl = request.ImageUrl;
        meal.Description = request.Description;
        meal.FixedCalories = request.FixedCalories;
        meal.FixedProtein = request.FixedProtein;
        meal.FixedCarbs = request.FixedCarbs;
        meal.FixedFats = request.FixedFats;
        meal.MealType = request.MealType;
        meal.AcceptCarb = request.AcceptCarb;
        meal.SubcategoryId = request.SubcategoryId;
        meal.IngredientId = request.IngredientId;
        meal.DefaultQuantityGrams = request.DefaultQuantityGrams;
        _unitOfWork.Meals.Update(meal);
        await _unitOfWork.CompleteAsync();
        return new ResultMessage
        {
            MealId = meal.Id,
            Message = "Meal updated successfully",
            Success = true
        };
    }
}
