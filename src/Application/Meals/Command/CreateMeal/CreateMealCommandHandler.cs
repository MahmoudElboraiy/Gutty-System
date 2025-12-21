

using Application.Authentication.Common.EditAddress;
using Application.Cache;
using Application.Interfaces;
using Application.Interfaces.UnitOfWorkInterfaces;
using Domain.Models.Entities;
using ErrorOr;
using MediatR;

namespace Application.Meals.Command.CreateMeal;

public class CreateMealCommandHandler : IRequestHandler<CreateMealCommand, ErrorOr<ResultMessage>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileStorageService _fileService;
    private readonly ICacheService _cacheService;
    public CreateMealCommandHandler(IUnitOfWork unitOfWork, IFileStorageService fileService, ICacheService cacheService)
    {
        _unitOfWork = unitOfWork;
        _fileService = fileService;
        _cacheService = cacheService;
    }
    public async Task<ErrorOr<ResultMessage>> Handle(CreateMealCommand request, CancellationToken cancellationToken)
    {
        if(request.Image == null || request.Image.Length == 0)
        {
            return Error.Validation("Meal.ImageMissing", "Image file is required for the meal.");
        }
        var imageUrl = await _fileService.SaveImageAsync(request.Image);
        var meal = new Meal
        {
            Name = request.Name,
            ImageUrl = imageUrl,
            Description = request.Description,
            FixedCalories = request.FixedCalories,
            FixedProtein = request.FixedProtein,
            FixedCarbs = request.FixedCarbs,
            FixedFats = request.FixedFats,
            MealType = request.MealType,
            AcceptCarb = request.AcceptCarb,
            SubcategoryId = request.SubcategoryId,
            IngredientId = request.IngredientId,
            DefaultQuantityGrams = request.DefaultQuantityGrams
        };
        _cacheService.IncrementVersion(CacheKeys.MealsVersion);
        await _unitOfWork.Meals.AddAsync(meal);
        await _unitOfWork.CompleteAsync();
        return new ResultMessage
        {
            MealId = meal.Id,
            Message = "Meal created successfully",
            Success = true
        };
    }
}
