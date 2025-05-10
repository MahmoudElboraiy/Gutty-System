using Application.Interfaces.UnitOfWorkInterfaces;
using Domain.DErrors;
using Domain.Models.Entities;
using Domain.Models.Identity;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Plans.Commands.CreatePlan;

public class CreatePlanCommandHandler
    : IRequestHandler<CreatePlanCommand, ErrorOr<CreatePlanCommandResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<User> _userManager;

    public CreatePlanCommandHandler(IUnitOfWork unitOfWork, UserManager<User> userManager)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
    }

    public async Task<ErrorOr<CreatePlanCommandResponse>> Handle(
        CreatePlanCommand request,
        CancellationToken cancellationToken
    )
    {
        var meals = new List<Meal>();
        decimal totalPrice = 0;

        foreach (var mealRequest in request.Meals)
        {
            var item = await _unitOfWork.Items.GetByIdAsync(mealRequest.ItemId);
            if (item == null)
            {
                return DomainErrors.Items.ItemNotFound(mealRequest.ItemId);
            }
            var totalPriceForMeal = (int)item.BasePrice * mealRequest.Quantity;

            ExtraItemOption? extraItemOption = null;
            if (mealRequest.ExtraItemOptionId.HasValue)
            {
                extraItemOption = await _unitOfWork.ExtraItemOptions.GetByIdAsync(
                    mealRequest.ExtraItemOptionId.Value
                );
                if (extraItemOption == null)
                {
                    return DomainErrors.Items.ExtraItemOptionNotFound(
                        mealRequest.ExtraItemOptionId.Value
                    );
                }
            }

            var meal = new Meal
            {
                ItemId = mealRequest.ItemId,
                Item = item,
                ExtraItemOptionId = mealRequest.ExtraItemOptionId,
                ExtraItemOption = extraItemOption,
                MealType = mealRequest.MealType,
                Quantity = (uint)mealRequest.Quantity,
            };

            // Add extra item option price if exists
            if (extraItemOption != null)
            {
                totalPriceForMeal += (int)extraItemOption.Price * mealRequest.Quantity;
            }

            totalPrice += totalPriceForMeal;
            meals.Add(meal);
        }

        var plan = new Plan
        {
            Name = request.Name,
            Description = request.Description,
            IsPreDefined = request.IsPreDefined,
            Meals = meals,
            TotalPrice = totalPrice,
        };

        await _unitOfWork.Plans.AddAsync(plan);
        await _unitOfWork.CompleteAsync();

        return new CreatePlanCommandResponse(plan.Id);
    }
}
