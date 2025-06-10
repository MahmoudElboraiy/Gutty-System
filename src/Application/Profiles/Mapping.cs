using Application.Ingredients.Queries.GetIngredients;
using Application.IngredientsChanges.Queries;
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
            user.PhoneNumber ?? "",
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
            item.NameAr,
            item.Description,
            item.DescriptionAr,
            item.Weight,
            item.Calories,
            item.Fats,
            item.Carbs,
            item.Proteins,
            item.Fibers,
            item.Type,
            item.ImageUrls,
            item.Ingredients.Select(x => x.MapRecipeIngredientResponse()).ToList(),
            item.WeightToPriceRatio
        );

    private static GetItemRecipeIngredientResponse MapRecipeIngredientResponse(
        this ItemIngredient recipeIngredient
    ) => new(recipeIngredient.IngredientId, recipeIngredient.Ingredient.Name, recipeIngredient.Ingredient.NameAr, recipeIngredient.Quantity, IsOptional: recipeIngredient.IsOptional);

    public static GetIngredientsQueryResponseItem MapIngredientResponse(
        this Ingredient ingredient
    ) => new(ingredient.Id, ingredient.Name, ingredient.NameAr, ingredient.StockQuantity);

    public static GetIngredientsChangesQueryResponseItem MapIngredientChangeResponse(
        this IngredientChange ingredientChange
    ) =>
        new(
            ingredientChange.CreatedAt,
            ingredientChange.Quantity,
            ingredientChange.OldValue,
            ingredientChange.NewValue,
            ingredientChange.Ingredient.MapIngredientChangeResponseIngredient()
        );

    private static GetIngredientsChangesResponseIngredientQuery MapIngredientChangeResponseIngredient(
        this Ingredient ingredient
    ) => new(ingredient.Id, ingredient.Name, ingredient.NameAr);
}
