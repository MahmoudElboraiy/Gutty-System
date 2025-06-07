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
            var totalPriceForMeal = item.BasePrice * mealRequest.Quantity;

            if (mealRequest.Weight < item.Weight)
            {
                return DomainErrors.Items.WeightMustBeGreaterThanMinWeight((int)item.Weight);
            }

            var meal = new Meal
            {
                ItemId = mealRequest.ItemId,
                Item = item,
                Weight = item.Weight,
                MealType = mealRequest.MealType,
                Quantity = (uint)mealRequest.Quantity,
            };

            if (mealRequest.Weight > item.Weight)
            {
                totalPriceForMeal  += (item.WeightToPriceRatio * (mealRequest.Weight - item.Weight));
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
