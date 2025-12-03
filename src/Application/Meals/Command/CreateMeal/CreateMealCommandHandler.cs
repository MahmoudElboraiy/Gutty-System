

using Application.Authentication.Common.EditAddress;
using Application.Interfaces.UnitOfWorkInterfaces;
using Domain.Models.Entities;
using ErrorOr;
using MediatR;

namespace Application.Meals.Command.CreateMeal;

public class CreateMealCommandHandler : IRequestHandler<CreateMealCommand, ErrorOr<ResultMessage>>
{
    private readonly IUnitOfWork _unitOfWork;
    public CreateMealCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<ErrorOr<ResultMessage>> Handle(CreateMealCommand request, CancellationToken cancellationToken)
    {
        var meal = new Meal
        {
            Name = request.Name,
            ImageUrl = request.ImageUrl,
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
