using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DErrors;

public static class MealErrors
{
    public static Error MealNotFound(int mealId) =>
        Error.NotFound(
            code: "Meal.NotFound",
            description: $"Meal with ID {mealId} was not found.");

    public static Error MealInUse(int mealId) =>
        Error.Conflict(
            code: "Meal.InUse",
            description: $"Meal with ID {mealId} is used in one or more plans and cannot be deleted.");

    public static Error MealCategoryNotFound(int categoryId) =>
        Error.NotFound(
            code: "MealCategory.NotFound",
            description: $"Meal category with ID {categoryId} was not found.");

    public static Error FoodNotFound(int foodId) =>
        Error.NotFound(
            code: "Food.NotFound",
            description: $"Food with ID {foodId} was not found.");

    public static Error FoodNotAvailable(int foodId, string foodName) =>
        Error.Conflict(
            code: "Food.NotAvailable",
            description: $"Food '{foodName}' (ID: {foodId}) is not available.");
}