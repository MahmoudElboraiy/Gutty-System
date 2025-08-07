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
        var plan = new Plan
        {
            Name = request.Name,
            Description = request.Description,
            DurationInDays = request.DurationInDays,
            LunchMealsPerDay = request.LunchMealsPerDay,
            DinnerMealsPerDay = request.DinnerMealsPerDay,
            BreakfastMealsPerDay = request.BreakfastMealsPerDay,
            MaxSeaFood = request.MaxSeaFood,
            MaxMeat = request.MaxMeat,
            MaxTwagen = request.MaxTwagen,
            MaxChicken = request.MaxChicken,
            MaxPizza = request.MaxPizza,
            MaxHighCarb = request.MaxHighCarb,
            Price = request.Price,
        };

        await _unitOfWork.Plans.AddAsync(plan);
        await _unitOfWork.CompleteAsync();

        return new CreatePlanCommandResponse(plan.Id);
    }
}
