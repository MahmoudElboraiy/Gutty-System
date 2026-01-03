

using Domain.Enums;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Meals.Command.UpdateMeal;

public record UpdateMealCommand(
    int Id,
    string Name,
    IFormFile? Image,
    string Description,
    decimal? FixedCalories,
    decimal? FixedProtein,
    decimal? FixedCarbs,
    decimal? FixedFats,
    MealType? MealType,
    bool AcceptCarb,
    int SubcategoryId,
    int? IngredientId,
    decimal? DefaultQuantityGrams
) : IRequest<ErrorOr<ResultMessage>>;
public record ResultMessage
{
    public int MealId { get; set; }
    public string Message { get; set; }
    public bool Success { get; set; }
}

