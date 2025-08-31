using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;

namespace Application.Subscriptions.Commands.PlaceOrder;

public record PlaceOrderCommand(
    string UserId,
    string PlanName,
    uint DurationInDays,
    uint NumberOfLunchMeals,
    decimal BreakfastPrice,
    decimal DinnerPrice,
    uint PastaCarbGrams,
    uint RiceCarbGrams,
    DateTime StartDate,
    bool IsActive,
    List<PlaceOrderPlanCategory> LunchCategories,
    Guid? PromoCodeId
) : IRequest<ErrorOr<PlaceOrderCommandResponse>>;

public record PlaceOrderPlanCategory(
    string Name,
    uint NumberOfMeals,
    uint ProteinGrams,
    decimal PricePerGram,
    bool AllowProteinChange,
    uint MaxProteinGrams,
    int? SubCategoryId
);

public record PlaceOrderCommandResponse(Guid SubscriptionId);
