using Application.Interfaces.UnitOfWorkInterfaces;
using Domain.Models.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class RecipeIngredientRepository : IRecipeIngredientRepository
{
    private readonly ApplicationDbContext _context;

    public RecipeIngredientRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task AddAsync(RecipeIngredient recipeIngredient)
    {
        _context.RecipeIngredients.Add(recipeIngredient);

        return Task.CompletedTask;
    }

    public Task UpdateAsync(RecipeIngredient recipeIngredient)
    {
        _context.RecipeIngredients.Update(recipeIngredient);

        return Task.CompletedTask;
    }

    public Task DeleteAsync(RecipeIngredient recipeIngredient)
    {
        _context.RecipeIngredients.Remove(recipeIngredient);

        return Task.CompletedTask;
    }

    public Task<RecipeIngredient> GetAsync(int recipeId, int ingredientId)
    {
        throw new NotImplementedException();
    }

    public Task<List<RecipeIngredient>> GetByRecipeIdAsync(int recipeId)
    {
        throw new NotImplementedException();
    }

    public Task<List<RecipeIngredient>> GetByIngredientIdAsync(int ingredientId)
    {
        throw new NotImplementedException();
    }

    public IQueryable GetAll()
    {
        throw new NotImplementedException();
    }
}
