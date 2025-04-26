using Application.Ingredients.Queries.GetIngredients;
using Application.Items.Queries.GetItem;
using Application.Users.Queries.GetUsers;
using Domain.Models.Entities;
using Domain.Models.Identity;

namespace Application.Profiles;

public static class Mapping
{
    public static UserResponse MapUserResponse(this User user) =>
        new(
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

    public static GetItemQueryResponse MapItemResponse(this Item item) =>
        new(
            item.Id,
            item.Name,
            item.Description,
            item.Weight ,
            item.Calories ,
            item.Fats,
            item.Carbs,
            item.Proteins,
            item.Fibers,
            item.Type,
            item.ImageUrls,
            item.Ingredients.Select(x => x.MapRecipeIngredientResponse()).ToList(),
            item.ExtraItemOptions == null ? [] : item.ExtraItemOptions.Select(x => x.MapExtraItemOptionsResponse()).ToList()
        );

    public static GetItemRecipeIngredientResponse MapRecipeIngredientResponse(
        this ItemIngredient recipeIngredient
    ) => new(recipeIngredient.IngredientId, recipeIngredient.Quantity);

    public static GetItemExtraItemOptionsResponse MapExtraItemOptionsResponse(
        this ExtraItemOption extraItemOption
    ) => new(extraItemOption.Price, extraItemOption.Weight);
    
    public static GetIngredientsQueryResponseItem MapIngredientResponse(this Ingredient ingredient) =>
        new(ingredient.Id, ingredient.Name,ingredient.StockQuantity);
}
