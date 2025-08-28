
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

 

    public static GetPlanQueryResponseItem MapPlanResponse(this Plan plan) =>
     new(
        plan.Id,
        plan.Name,
        plan.Description,
        plan.DurationInDays,
        plan.NumberOfLunchMeals,
        plan.BreakfastPrice,
        plan.DinnerPrice,
        plan.GetTotalPrice(),
        plan.RiceCarbGrams,
        plan.PastaCarbGrams,
        plan.MaxRiceCarbGrams,
        plan.MaxPastaCarbGrams,
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
