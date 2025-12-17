
using Application.Interfaces.UnitOfWorkInterfaces;
using Application.Plans.Queries.NewFolder1;
using Domain.Enums;
using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Plans.Queries.Calculate;

public class CalculatePlanPriceQueryHandler
    : IRequestHandler<CalculatePlanPriceQuery, ErrorOr<CalculatePlanPriceResponse>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CalculatePlanPriceQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<CalculatePlanPriceResponse>> Handle(CalculatePlanPriceQuery request, CancellationToken cancellationToken)
    {
        var plan = await _unitOfWork
            .Plans
            .GetQueryable()
            .Include(p => p.LunchCategories)
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == request.PlanId, cancellationToken);

        if (plan is null)
            return Error.NotFound("Plan.NotFound", $"Plan {request.PlanId} not found.");

        var carb = request.CarbGrams ?? plan.CarbGrams;

        if (carb > plan.MaxCarbGrams)
            return Error.Validation("CarbGrams.TooHigh", $"Carbs cannot exceed {plan.MaxCarbGrams} grams.");


        var resultItems = new List<CategoryCalculationItemDto>();
        decimal sumCategories = 0m;

        foreach (var cat in plan.LunchCategories)
        {
            var mod = request.Categories?.FirstOrDefault(c => c.CategoryId == cat.Id);
            uint requestedMeals = mod?.NumberOfMeals ?? cat.NumberOfMeals;
            uint requestedProtein = mod?.ProteinGrams ?? cat.ProteinGrams;

            if (!cat.AllowProteinChange)
                requestedProtein = cat.ProteinGrams;


            if (requestedProtein > cat.MaxProteinGrams)
                return Error.Validation("Category.ProteinTooHigh", $"Category '{cat.Name}' protein cannot exceed {cat.MaxProteinGrams}g.");

            decimal categoryPrice = requestedMeals * requestedProtein * cat.PricePerGram;

            resultItems.Add(new CategoryCalculationItemDto(
                cat.Id,
                cat.Name,
                requestedMeals,
                requestedProtein,
                categoryPrice
            ));

            sumCategories += categoryPrice; 
        }

        decimal total = plan.BreakfastPrice + plan.DinnerPrice + sumCategories;

        decimal discountAmount = 0;

        if (!string.IsNullOrEmpty(request.PromoCode))
        {
            var promo = (await _unitOfWork.PromoCodes.FindAsync(p => p.Code == request.PromoCode)).FirstOrDefault();

            if (promo == null || !promo.IsActive || promo.ExpiryDate < DateTime.UtcNow)
            {
                return Error.Validation("PromoCode.Is.InValid", $"The PromoCode '{request.PromoCode}'is InValid");
            }
            else
            {

                    if (promo.DiscountType == DiscountType.Percentage)
                        discountAmount = total * (promo.DiscountValue / 100m);
                    else
                        discountAmount = promo.DiscountValue;

                    total -= discountAmount;    
                
            }
        }
        return new CalculatePlanPriceResponse(
            TotalPrice: total
            //BreakfastPrice: plan.BreakfastPrice,
            //DinnerPrice: plan.DinnerPrice,
            //CarbGrams: carb,
            //Categories: resultItems
        );
    }
}
