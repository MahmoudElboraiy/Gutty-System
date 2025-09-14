//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using ErrorOr;
//using MediatR;

//namespace Application.Meals.Commands.Create;
//public record CreateMealCommand
//    (
//    string Name,
//    string Description,
//    decimal Price,
//    int Calories,
//    int Protein,
//    int Carbohydrates,
//    int Fats,
//    List<string> Ingredients,
//    List<string> Tags
//);


using ErrorOr;
using MediatR;

namespace Application.Meals.Commands.Create;

public record CreateMealCommand : IRequest<ErrorOr<CreateMealCommandResponse>>
{
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public decimal Price { get; init; }
    public string ImageUrl { get; init; } = string.Empty;
    public int MealCategoryId { get; init; }
    public int PreparationTime { get; init; }
    public int Calories { get; init; }
    public List<MealFoodItem> Foods { get; init; } = new();
}

public record MealFoodItem
{
    public int FoodId { get; init; }
    public int Quantity { get; init; }
}

