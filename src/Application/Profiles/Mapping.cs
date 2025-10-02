
using Application.Orders.Query.ShowOrderDetails;
using Application.Plans.Queries.GetPlans;
using Application.Users.Queries.GetUsers;
using Domain.Models.Entities;
using Domain.Models.Identity;

namespace Application.Profiles;

public static class Mapping
{
    public static UserResponse MapUserResponse(this User user) =>
        new(
            user.Id,
            user.PhoneNumber ?? "",
            user.FirstName,
            user.MiddleName,
            user.LastName,
            user.MainAddress,
            user.SecondPhoneNumber,
            user.Email,
            user.PhoneNumberConfirmed
        );




    //public static GetIngredientsQueryResponseItem MapIngredientResponse(
    //    this Ingredient ingredient
    //) => new(ingredient.Id, ingredient.Name, ingredient.NameAr, ingredient.StockQuantity);

    public static ShowOrderMealsDetailsQueryResponseItem MapOrderMealResponse(
       this OrderMeal orderMeal,
       Dictionary<int, (string Name, string ImageUrl, bool AcceptCarb)> mealDetailsDict)
    {
        // äÌíÈ ÊÝÇÕíá ÇáÜ meal (ÓæÇÁ main / protein / carb)
        string mealName = null;
        string mealImage = null;
        bool acceptCarb = false;

        if (orderMeal.MealId.HasValue && mealDetailsDict.TryGetValue(orderMeal.MealId.Value, out var mainMeal))
        {
            mealName = mainMeal.Name;
            mealImage = mainMeal.ImageUrl;
            acceptCarb = mainMeal.AcceptCarb;
        }
        else if (orderMeal.ProteinMealId.HasValue && mealDetailsDict.TryGetValue(orderMeal.ProteinMealId.Value, out var proteinMeal))
        {
            mealName = proteinMeal.Name;
            mealImage = proteinMeal.ImageUrl;
            acceptCarb = proteinMeal.AcceptCarb;
        }
        else if (orderMeal.CarbMealId.HasValue && mealDetailsDict.TryGetValue(orderMeal.CarbMealId.Value, out var carbMeal))
        {
            mealName = carbMeal.Name;
            mealImage = carbMeal.ImageUrl;
            acceptCarb = carbMeal.AcceptCarb;
        }

        return new ShowOrderMealsDetailsQueryResponseItem(
            orderMeal.Id,
            mealName ?? "Unknown Meal",
            mealImage ?? string.Empty,
            orderMeal.MealId,
            orderMeal.ProteinMealId,
            orderMeal.CarbMealId,
            acceptCarb,
            orderMeal.Notes
        );
    }

    public static GetPlanQueryResponseItem MapPlanResponse(this Plan plan) =>
     new(
        plan.Id,
        plan.Name,
        plan.Description,
        plan.DurationInDays,
        plan.LMealsPerDay,
        plan.BDMealsPerDay,
        plan.BreakfastPrice,
        plan.DinnerPrice,
        plan.GetTotalPrice(),
        plan.CarbGrams,
        plan.MaxCarbGrams,
        plan.LunchCategories.Select(c => new GetPlanCategoryResponseItem(
            c.Id,
            c.Name,
            c.NumberOfMeals,
            c.ProteinGrams,
            c.PricePerGram,
            c.AllowProteinChange,  
            c.MaxProteinGrams,
            c.GetCategoryPrice()
        )).ToList()
    );
}
