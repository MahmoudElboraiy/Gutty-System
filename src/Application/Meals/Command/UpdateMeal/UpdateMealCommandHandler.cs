

using Application.Interfaces.UnitOfWorkInterfaces;
using ErrorOr;
using MediatR;

namespace Application.Meals.Command.UpdateMeal;

public class UpdateMealCommandHandler : IRequestHandler<UpdateMealCommand, ErrorOr<ResultMessage>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileStorageService _fileService;
    public UpdateMealCommandHandler(IUnitOfWork unitOfWork, IFileStorageService fileService)
    {
        _unitOfWork = unitOfWork;
        _fileService = fileService;
    }
    public async Task<ErrorOr<ResultMessage>> Handle(UpdateMealCommand request, CancellationToken cancellationToken)
    {
        var meal = await _unitOfWork.Meals.GetByIdAsync(request.Id);
        if (meal == null)
        {
            return Error.NotFound("Meal.NotFound", "Meal not found");
        }
        if(request.Image ==null || request.Image.Length == 0)
        {
            return Error.Validation("Meal.ImageMissing", "Image file is required for the meal.");
        }
        var imageUrl = await _fileService.SaveImageAsync(request.Image);
        meal.Name = request.Name;
        meal.ImageUrl = imageUrl;
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
