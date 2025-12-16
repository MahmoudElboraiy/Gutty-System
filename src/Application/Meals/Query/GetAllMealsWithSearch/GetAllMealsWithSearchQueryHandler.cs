

using Application.Interfaces.UnitOfWorkInterfaces;
using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Meals.Query.GetAllMealsWithSearch;

public class GetAllMealsWithSearchQueryHandler: IRequestHandler<GetAllMealsWithSearchQuery, ErrorOr<GetAllMealsWithSearchQueryResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    public GetAllMealsWithSearchQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<ErrorOr<GetAllMealsWithSearchQueryResponse>> Handle(GetAllMealsWithSearchQuery request, CancellationToken cancellationToken)
    {
        var query = _unitOfWork.Meals
            .GetQueryable()
            .AsNoTracking()
            .Include(m => m.Ingredient)
            .Include(m => m.Subcategory)
            .AsQueryable();


        if (!string.IsNullOrWhiteSpace(request.searchName))
        {
            string searchLower = request.searchName.ToLower();
            query = query.Where(m => m.Name.ToLower().Contains(searchLower));
        }

     
        if (!string.IsNullOrWhiteSpace(request.SubCategoryName))
        {
            string subCatLower = request.SubCategoryName.ToLower();
            query = query.Where(m => m.Subcategory.Name.ToLower().Contains(subCatLower));
        }

       
        var totalCount = await query.CountAsync(cancellationToken);

       
        var mealsPaged = await query
            .OrderBy(m => m.Name)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

      
        var responseItems = mealsPaged.Select(m => new GetAllMealsWithSearchItem
        (
            m.Id,
            m.Name,
            m.Description,
            m.ImageUrl,
            m.FixedCalories ?? (m.Ingredient != null && m.DefaultQuantityGrams.HasValue
                ? (m.Ingredient.CaloriesPer100g * m.DefaultQuantityGrams.Value) / 100
                : 0),
            m.FixedProtein ?? (m.Ingredient != null && m.DefaultQuantityGrams.HasValue
                ? (m.Ingredient.ProteinPer100g * m.DefaultQuantityGrams.Value) / 100
                : 0),
            m.FixedCarbs ?? (m.Ingredient != null && m.DefaultQuantityGrams.HasValue
                ? (m.Ingredient.CarbsPer100g * m.DefaultQuantityGrams.Value) / 100
                : 0),
            m.FixedFats ?? (m.Ingredient != null && m.DefaultQuantityGrams.HasValue
                ? (m.Ingredient.FatsPer100g * m.DefaultQuantityGrams.Value) / 100
                : 0),
            m.MealType,
            m.Subcategory.Name,
            m.DefaultQuantityGrams
        )).ToList();

        var result = new GetAllMealsWithSearchQueryResponse(
            request.PageNumber,
            request.PageSize,
            totalCount,
            responseItems
        );

        return result;
    }
}
