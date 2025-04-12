using Domain.Models.Entities;

namespace Application.Interfaces.UnitOfWorkInterfaces;

public interface IRecipeIngredientRepository
{
    Task AddAsync(RecipeIngredient recipeIngredient);
    Task UpdateAsync(RecipeIngredient recipeIngredient);
    Task DeleteAsync(RecipeIngredient recipeIngredient);
    Task<RecipeIngredient> GetAsync(int recipeId, int ingredientId);
    Task<List<RecipeIngredient>> GetByRecipeIdAsync(int recipeId);
    Task<List<RecipeIngredient>> GetByIngredientIdAsync(int ingredientId);
    IQueryable GetAll();
}