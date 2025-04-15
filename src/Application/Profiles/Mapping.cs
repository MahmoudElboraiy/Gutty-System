using Application.Items.Queries.GetItem;
using Application.Users.Queries.GetUsers;
using Domain.Models.Entities;
using Domain.Models.Identity;

namespace Application.Profiles;

public static class Mapping
{
    public static UserResponse MapUserResponse(this User user) => new(
        user.Id,
        user.PhoneNumber,
        user.FirstName,
        user.MiddleName,
        user.LastName,
        user.MainAddress,
        user.SecondPhoneNumber,
        user.Email,
        user.PhoneNumberConfirmed
    );

    public static GetItemQueryResponse MapItemResponse(this Item item) => new(
        item.Id,
        item.Name,
        item.Description,
        item.WeightRaw ?? 0,
        item.Weight ?? 0,
        item.Calories ?? 0,
        item.Fats,
        item.Carbohydrates,
        item.Proteins,
        item.IsMainItem,
        item.RecipeIngredients.Select(x => x.MapRecipeIngredientResponse()).ToList()
    );
    public static GetItemRecipeIngredientResponse MapRecipeIngredientResponse(this RecipeIngredient recipeIngredient) => new(
        recipeIngredient.IngredientId,
        recipeIngredient.Quantity
    );

}