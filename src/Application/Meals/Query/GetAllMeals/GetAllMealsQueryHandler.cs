

using Application.Interfaces.UnitOfWorkInterfaces;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Application.Meals.Query.GetAllMeals;

public class GetAllMealsQueryHandler : IRequestHandler<GetAllMealsQuery, ErrorOr<List<GetAllMealsQueryResponse>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public GetAllMealsQueryHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _httpContextAccessor = httpContextAccessor;
    }
    public async Task<ErrorOr<List<GetAllMealsQueryResponse>>> Handle(GetAllMealsQuery request, CancellationToken cancellationToken)
    {
        var meals = await _unitOfWork.Meals
            .GetQueryable()
            .AsNoTracking()
            .Include(m => m.Ingredient)
            .Include(m => m.Subcategory)
            .ToListAsync(cancellationToken);
        var pagedMeals = meals.
            Skip((request.pageNumber - 1) * request.pageSize)
            .Take(request.pageSize)
            .ToList();

        var httpRequest = _httpContextAccessor.HttpContext!.Request;
        var baseUrl = $"{httpRequest.Scheme}://{httpRequest.Host}";

        var response = pagedMeals.Select(m => new GetAllMealsQueryResponse
        {
            Id = m.Id,
            Name = m.Name,
            Description = m.Description,
            ImageUrl = baseUrl + m.ImageUrl,
            Calories = m.FixedCalories ?? (m.Ingredient != null && m.DefaultQuantityGrams.HasValue
                ? (m.Ingredient.CaloriesPer100g * m.DefaultQuantityGrams.Value) / 100
                : 0),
            Protein = m.FixedProtein ?? (m.Ingredient != null && m.DefaultQuantityGrams.HasValue
                ? (m.Ingredient.ProteinPer100g * m.DefaultQuantityGrams.Value) / 100
                : 0),
            Carbs = m.FixedCarbs ?? (m.Ingredient != null && m.DefaultQuantityGrams.HasValue
                ? (m.Ingredient.CarbsPer100g * m.DefaultQuantityGrams.Value) / 100
                : 0),
            Fats = m.FixedFats ?? (m.Ingredient != null && m.DefaultQuantityGrams.HasValue
                ? (m.Ingredient.FatsPer100g * m.DefaultQuantityGrams.Value) / 100
                : 0),
            MealType = m.MealType,
            SubcategoryName = m.Subcategory.Name,
            DefaultQuantityGrams = m.DefaultQuantityGrams
        }).ToList();
        return response;
    }
}
