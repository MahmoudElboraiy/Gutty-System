
//using Application.Interfaces.UnitOfWorkInterfaces;
//using MediatR;
//using Microsoft.EntityFrameworkCore;

//namespace Application.Plans.Queries.NewFolder1;

//public class CalculatePlanPriceQueryHandler : IRequestHandler<CalculatePlanPriceQuery, CalculatePlanPriceResponse>
//{
//    private readonly IUnitOfWork _unitOfWork;

//    public CalculatePlanPriceQueryHandler(IUnitOfWork unitOfWork)
//    {
//        _unitOfWork = unitOfWork;
//    }

//    public async Task<CalculatePlanPriceResponse> Handle(CalculatePlanPriceQuery request, CancellationToken cancellationToken)
//    {
//        var plan = await _unitOfWork
//                    .Plans
//                    .GetQueryable()
//                      .Include(p => p.LunchCategories)
//                       .AsNoTracking()
//                        .FirstOrDefaultAsync(p => p.Id == request.PlanId, cancellationToken);
//        if (plan == null)
//            throw new KeyNotFoundException($"Plan {request.PlanId} not found.");

//        var rice = request.RiceCarbGrams ?? plan.RiceCarbGrams;
//        var pasta = request.PastaCarbGrams ?? plan.PastaCarbGrams;

//        if(rice < 0 || pasta <0)
//            throw new ArgumentException($"Carbs cannot be less than Zero.");

//        if (rice > plan.MaxRiceCarbGrams )
//            throw new ArgumentException($"Rice carbs cannot exceed {plan.MaxRiceCarbGrams} grams.");
//        if (pasta > plan.MaxPastaCarbGrams)
//            throw new ArgumentException($"Pasta carbs cannot exceed {plan.MaxPastaCarbGrams} grams.");

//        var resultItems = new List<CategoryCalculationItemDto>();
//        decimal sumCategories = 0m;

//        foreach (var cat in plan.LunchCategories)
//        {

//            var mod = request.Categories?.FirstOrDefault(c => c.CategoryId == cat.Id);


//            uint requestedMeals = mod?.NumberOfMeals ?? cat.NumberOfMeals;
//            uint requestedProtein = mod?.ProteinGrams ?? cat.ProteinGrams;


//            if (!cat.AllowProteinChange)
//                requestedProtein = cat.ProteinGrams;


//            if (requestedMeals < 0)
//                throw new ArgumentException($"Category '{cat.Name}' meals cannot be less than Zero.");

//            if (requestedProtein < cat.ProteinGrams)
//                throw new ArgumentException($"Category '{cat.Name}' protein cannot be less than default ({cat.ProteinGrams}g).");


//            if (requestedMeals > cat.MaxMeals)
//                throw new ArgumentException($"Category '{cat.Name}' meals cannot exceed max {cat.MaxMeals}.");
//            if (requestedProtein > cat.MaxProteinGrams)
//                throw new ArgumentException($"Category '{cat.Name}' protein cannot exceed max {cat.MaxProteinGrams}g.");

//            decimal categoryPrice = (decimal)requestedMeals * (decimal)requestedProtein * cat.PricePerGram;

//            resultItems.Add(new CategoryCalculationItemDto(
//                cat.Id,
//                cat.Name,
//                requestedMeals,
//                requestedProtein,
//                categoryPrice
//            ));

//            sumCategories += categoryPrice;
//        }

//        decimal total = plan.BreakfastPrice + plan.DinnerPrice + sumCategories;

//        return new CalculatePlanPriceResponse(
//            TotalPrice: total,
//            BreakfastPrice: plan.BreakfastPrice,
//            DinnerPrice: plan.DinnerPrice,
//            RiceCarbGrams: rice,
//            PastaCarbGrams: pasta,
//            Categories: resultItems
//        );
//    }
//}
using Application.Interfaces.UnitOfWorkInterfaces;
using Application.Plans.Queries.NewFolder1;
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

        var rice = request.RiceCarbGrams ?? plan.RiceCarbGrams;
        var pasta = request.PastaCarbGrams ?? plan.PastaCarbGrams;

        if (rice > plan.MaxRiceCarbGrams)
            return Error.Validation("RiceCarbGrams.TooHigh", $"Rice carbs cannot exceed {plan.MaxRiceCarbGrams} grams.");

        if (pasta > plan.MaxPastaCarbGrams)
            return Error.Validation("PastaCarbGrams.TooHigh", $"Pasta carbs cannot exceed {plan.MaxPastaCarbGrams} grams.");

        var resultItems = new List<CategoryCalculationItemDto>();
        decimal sumCategories = 0m;

        foreach (var cat in plan.LunchCategories)
        {
            var mod = request.Categories?.FirstOrDefault(c => c.CategoryId == cat.Id);

            uint requestedMeals = mod?.NumberOfMeals ?? cat.NumberOfMeals;
            uint requestedProtein = mod?.ProteinGrams ?? cat.ProteinGrams;

            if (!cat.AllowProteinChange)
                requestedProtein = cat.ProteinGrams;

            if (requestedMeals > cat.MaxMeals)
                return Error.Validation("Category.MealsTooHigh", $"Category '{cat.Name}' meals cannot exceed {cat.MaxMeals}.");

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

        return new CalculatePlanPriceResponse(
            TotalPrice: total,
            BreakfastPrice: plan.BreakfastPrice,
            DinnerPrice: plan.DinnerPrice,
            RiceCarbGrams: rice,
            PastaCarbGrams: pasta,
            Categories: resultItems
        );
    }
}
