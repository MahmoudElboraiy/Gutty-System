using Application.Interfaces.UnitOfWorkInterfaces;
using Domain.DErrors;
using Domain.Models.Entities;
using ErrorOr;
using MediatR;

namespace Application.Subscriptions.Commands.PlaceOrder;

public class PlaceOrderCommandHandler : IRequestHandler<PlaceOrderCommand, ErrorOr<PlaceOrderCommandResponse>>
{
    private readonly IUnitOfWork _unitOfWork;

    public PlaceOrderCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<PlaceOrderCommandResponse>> Handle(PlaceOrderCommand request, CancellationToken cancellationToken)
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
            PlanName = request.PlanName,
            DurationInDays = request.DurationInDays,
            BreakfastPrice = request.BreakfastPrice,
            DinnerPrice = request.DinnerPrice,
            PastaCarbGrams = request.PastaCarbGrams,
            RiceCarbGrams = request.RiceCarbGrams,
            MaxRiceCarbGrams = request.MaxRiceCarbGrams,
            MaxPastaCarbGrams = request.MaxPastaCarbGrams,
            StartDate = request.StartDate,
            IsActive = request.IsActive,
            PromoCodeId = request.PromoCodeId,
            LunchCategories = request.LunchCategories.Select(c => new SubscriptionCategory
            {
                CategoryId = c.CategoryId,
                Name = c.Name,
                NumberOfMeals = c.NumberOfMeals,
                ProteinGrams = c.ProteinGrams,
                PricePerGram = c.PricePerGram,
                AllowProteinChange = c.AllowProteinChange,
                MaxMeals = c.MaxMeals,
                MaxProteinGrams = c.MaxProteinGrams
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

        return new PlaceOrderCommandResponse(subscription.Id);
    }
}
