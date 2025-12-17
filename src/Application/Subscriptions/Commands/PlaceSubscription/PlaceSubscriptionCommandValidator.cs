using Application.Subscriptions.Commands.PlaceOrder;
using FluentValidation;

namespace Application.Subscriptions.Commands.PlaceSubscription;

public class PlaceSubscriptionCommandValidator : AbstractValidator<PlaceSubscriptionCommand>
{
    public PlaceSubscriptionCommandValidator()
    {
        //RuleFor(x => x.UserId).NotEmpty();
        //RuleFor(x => x.PlanName).NotEmpty().MaximumLength(255);  
        //RuleFor(x => x.BreakfastPrice).GreaterThanOrEqualTo(0);
        //RuleFor(x => x.DinnerPrice).GreaterThanOrEqualTo(0);
        //RuleFor(x => x.MaxRiceCarbGrams).GreaterThanOrEqualTo(x => x.RiceCarbGrams);
        //RuleFor(x => x.MaxPastaCarbGrams).GreaterThanOrEqualTo(x => x.PastaCarbGrams);
    }
}
