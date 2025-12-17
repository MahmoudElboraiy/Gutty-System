using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;

namespace Application.Subscriptions.Commands.PlaceOrder;

public record PlaceSubscriptionCommand(
    string UserId,
    Guid PlanId,
    uint DaysLeft,
    uint LunchMealsLeft,
    uint CarbGrams,
    DateTime StartDate,
    bool IsCurrent,
    bool IsPaused,
    List<PlaceSubscriptionPlanCategory> LunchCategories,
    string? PromoCode
) : IRequest<ErrorOr<PlaceSubscriptionCommandResponse>>;

public record PlaceSubscriptionPlanCategory(
    uint NumberOfMeals,
    uint NumberOfMealsLeft,
    uint ProteinGrams,
    decimal PricePerGram,
    bool AllowProteinChange,
    uint MaxProteinGrams,
    int? SubCategoryId
);

public record PlaceSubscriptionCommandResponse(Guid SubscriptionId);
