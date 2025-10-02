using Application.Interfaces.UnitOfWorkInterfaces;
using Domain.DErrors;
using Domain.Models.Entities;
using ErrorOr;
using MediatR;

namespace Application.Subscriptions.Commands.PlaceOrder;

public class PlaceSubscriptionCommandHandler : IRequestHandler<PlaceSubscriptionCommand, ErrorOr<PlaceSubscriptionCommandResponse>>
{
    private readonly IUnitOfWork _unitOfWork;

    public PlaceSubscriptionCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<PlaceSubscriptionCommandResponse>> Handle(PlaceSubscriptionCommand request, CancellationToken cancellationToken)
    {
        PromoCode? promo = null;
        if (request.PromoCodeId.HasValue)
        {
            promo = await _unitOfWork.PromoCodes.GetByIdAsync(request.PromoCodeId.Value);
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
            PromoCodeId = request.PromoCodeId,
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
