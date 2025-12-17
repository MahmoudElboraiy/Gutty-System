

using Application.Interfaces.UnitOfWorkInterfaces;
using Domain.Enums;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Application.Meals.Query.GetMealDetails;

public class GetMealDetailsQueryHandler : IRequestHandler<GetMealDetailsQuery, ErrorOr<GetMealDetailsQueryResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public GetMealDetailsQueryHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _httpContextAccessor = httpContextAccessor;
    }
    public async Task<ErrorOr<GetMealDetailsQueryResponse>> Handle(GetMealDetailsQuery request, CancellationToken cancellationToken)
    {
        var meal = await _unitOfWork.Meals
            .GetQueryable()
            .Where(m => m.Id == request.MealId)
            .AsNoTracking()
            .Include(i=>i.Ingredient)
            .FirstOrDefaultAsync(cancellationToken);

        var category = await _unitOfWork.SubCategories
            .GetQueryable()
            .Where(c => c.Id == meal.SubcategoryId)
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken);

        GetMealDetailsQueryResponse responseItem = null;
        if(meal == null) {
            return Error.NotFound("Meal.NotFound", "Meal not found");
        }
        var httpRequest = _httpContextAccessor.HttpContext!.Request;
        var baseUrl = $"{httpRequest.Scheme}://{httpRequest.Host}";
        if (meal.IngredientId == null)
        {
            responseItem = new GetMealDetailsQueryResponse
            (
                meal.Id,
                meal.Name,
                meal.Description,
                baseUrl + meal.ImageUrl,
                meal.FixedCalories.Value,
                meal.FixedProtein.Value,
                meal.FixedCarbs.Value,
                meal.FixedFats.Value,
                category.CategoryId,
                meal.SubcategoryId,
                meal.DefaultQuantityGrams
            );
            return responseItem;
        }

        var subscription = await _unitOfWork.Subscriptions
            .GetQueryable()
            .AsNoTracking()
            .Where(s => s.UserId == request.UserId && s.IsCurrent)
            .Include(s => s.LunchCategories)          
            .FirstOrDefaultAsync(cancellationToken);

        if (subscription == null)
        {
        }
        decimal ingredient =100;
        var subscriptionCategory = subscription?.LunchCategories
                .FirstOrDefault(lc => lc.SubCategoryId == meal.SubcategoryId);

        if (subscriptionCategory == null || meal.MealType == null)
        {
            ingredient = meal.DefaultQuantityGrams ?? 100;
        }
        else if(meal.MealType == MealType.Protien)
        {  
            ingredient = subscriptionCategory.ProteinGrams;
        }else if(meal.MealType ==MealType.Carb)
            ingredient =subscription.CarbGrams;

        decimal ratio = ingredient / 100;


        responseItem =new GetMealDetailsQueryResponse
        (
            meal.Id,
            meal.Name,
            meal.Description,
            baseUrl + meal.ImageUrl,
            meal.Ingredient.CaloriesPer100g * ratio,
            meal.Ingredient.ProteinPer100g * ratio,
            meal.Ingredient.CarbsPer100g * ratio,
            meal.Ingredient.FatsPer100g * ratio,
            category.CategoryId,
            meal.SubcategoryId,
            ingredient
        );
        return responseItem;
    }
}
