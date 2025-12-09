using Application.Interfaces;
using Application.Interfaces.UnitOfWorkInterfaces;
using Domain.DErrors;
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


        await _unitOfWork.Subscriptions.AddAsync(subscription);

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
}
