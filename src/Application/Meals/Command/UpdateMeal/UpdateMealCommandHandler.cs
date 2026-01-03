

using Application.Cache;
using Application.Interfaces;
using Application.Interfaces.UnitOfWorkInterfaces;
using ErrorOr;
using MediatR;

namespace Application.Meals.Command.UpdateMeal;

public class UpdateMealCommandHandler : IRequestHandler<UpdateMealCommand, ErrorOr<ResultMessage>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileStorageService _fileService;
    private readonly ICacheService _cacheService;
    public UpdateMealCommandHandler(IUnitOfWork unitOfWork, IFileStorageService fileService, ICacheService cacheService)
    {
        _unitOfWork = unitOfWork;
        _fileService = fileService;
        _cacheService = cacheService;
    }
    public async Task<ErrorOr<ResultMessage>> Handle(UpdateMealCommand request, CancellationToken cancellationToken)
    {
        var meal = await _unitOfWork.Meals.GetByIdAsync(request.Id);
        if (meal == null)
        {
            return Error.NotFound("Meal.NotFound", "Meal not found");
        }
        var imageUrl = meal.ImageUrl;

        if(request.Image != null)
             imageUrl = await _fileService.SaveImageAsync(request.Image);

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
        _cacheService.IncrementVersion(CacheKeys.MealsVersion);
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
