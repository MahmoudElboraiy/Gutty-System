using Application.Interfaces;
using Application.Interfaces.UnitOfWorkInterfaces;
using Domain.DErrors;
using Domain.Enums;
using Domain.Models.Entities;
using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Subscriptions.Commands.PlaceOrder;

public class PlaceSubscriptionCommandHandler : IRequestHandler<PlaceSubscriptionCommand, ErrorOr<PlaceSubscriptionCommandResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;

    public PlaceSubscriptionCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
    {
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
    }

    public async Task<ErrorOr<PlaceSubscriptionCommandResponse>> Handle(PlaceSubscriptionCommand request, CancellationToken cancellationToken)
    {
       // var subscriptionExist = await _unitOfWork.Subscriptions
         ///   .FindAsync(s => s.UserId == request.UserId && s.IsCurrent);
        var subscriptionExist = await _unitOfWork.Subscriptions.GetQueryable()
            .AnyAsync(s => s.UserId == request.UserId && s.IsCurrent, cancellationToken);
        if (subscriptionExist == true)
        {
            return Error.Validation("Subscription.AlreadyExists", "User already has an active subscription.");
        }
        var plan = await _unitOfWork.Plans
            .GetQueryable()
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == request.PlanId, cancellationToken);
        if (plan is null)
        {
            return Error.Validation("Plan.NotFound", "Subscription plan not found.");
        }

        PromoCode? promo = null;
        if (request.PromoCode!=null)
        {
            promo = await _unitOfWork.PromoCodes
                .GetQueryable()
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Code == request.PromoCode);
            if (promo is null || !promo.IsActive || promo.ExpiryDate < DateTime.UtcNow)
            {
                return Error.Validation("PromoCode.InvalidOrExpired", "Invalid or expired promo code.");
            }
        }
        var numberOfMealsSelected = request.LunchCategories.Sum(c => c.NumberOfMeals);
        if (numberOfMealsSelected != plan.DurationInDays*plan.LMealsPerDay)
        {
            return Error.Validation("Subscription.InvalidMealsCount", $"The total number of lunch meals selected ({numberOfMealsSelected}) does not match the plan's allowed lunch meals ({plan.DurationInDays * plan.LMealsPerDay}).");

        }
        var subscription = new Subscription
        {
            UserId = request.UserId,
            PlanId =request.PlanId,
            DaysLeft =request.DaysLeft,
            LunchMealsLeft = request.LunchMealsLeft,
            CarbGrams = request.CarbGrams,
            StartDate = request.StartDate,
            PromoCodeId = promo?.Id,
            IsCurrent = request.IsCurrent,
            IsPaused = request.IsPaused,
            LunchCategories = request.LunchCategories.Select(c => new SubscriptionCategory
            {
                SubCategoryId = c.SubCategoryId,
                NumberOfMeals = c.NumberOfMeals,
                NumberOfMealsLeft = c.NumberOfMeals,
                ProteinGrams = c.ProteinGrams,
                PricePerGram = c.PricePerGram,
                AllowProteinChange = c.AllowProteinChange,
                MaxProteinGrams = c.MaxProteinGrams,   
            }).ToList()
        };

        //decimal total = subscription.GetTotalPrice();
        decimal total = CalculateSubscriptionPrice(plan, request);
        decimal discountAmount = 0;

        if(promo is not null)
        {
            if (promo.DiscountType == DiscountType.Percentage)
                discountAmount = total * (promo.DiscountValue / 100m);
            else
                discountAmount = promo.DiscountValue;
        }
        total -= discountAmount;
        var sale = new Sales
        {
            ItemType = SaleType.Subscription,
            ItemName = plan.Name,
            Quantity = 1,
            UnitType = UnitType.Package,
            Price = total,
            CustomerId = request.UserId,
            SaleDate = DateOnly.FromDateTime(DateTime.UtcNow)
        };
        await _unitOfWork.Subscriptions.AddAsync(subscription);
        await _unitOfWork.Sales.AddAsync(sale);

        if (promo is not null)
        {
            await _unitOfWork.PromoCodeUsages.AddAsync(new PromoCodeUsage
            {
                Id = Guid.NewGuid(),
                PromoCodeId = promo.Id,
                UserId = request.UserId,
                UsedAt = DateTime.UtcNow
            });
        }

        await _unitOfWork.CompleteAsync();

        return new PlaceSubscriptionCommandResponse(subscription.Id);
    }
    private decimal CalculateSubscriptionPrice(Plan plan, PlaceSubscriptionCommand request)
    {
        decimal price = plan.BreakfastPrice + plan.DinnerPrice;

        price += request.LunchCategories.Sum(c =>
            c.NumberOfMeals * c.PricePerGram * c.ProteinGrams
        );

        return price;
    }

}

