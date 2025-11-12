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
            LMealsPerDay = request.LMealsPerDay,
            BDMealsPerDay = request.BDMealsPerDay,
            BreakfastPrice = request.BreakfastPrice,
            DinnerPrice = request.DinnerPrice,
            CarbGrams = request.CarbGrams,
            MaxCarbGrams = request.MaxCarbGrams,
            LunchCategories = request.LunchCategories.Select(c => new PlanCategory
            {
                Name = c.Name,
                NumberOfMeals = c.NumberOfMeals,
                ProteinGrams = c.ProteinGrams,
                PricePerGram = c.PricePerGram,
                AllowProteinChange = c.AllowProteinChange,
                MaxProteinGrams = c.MaxProteinGrams
            }).ToList()
        };

        await _unitOfWork.Plans.AddAsync(plan);
        await _unitOfWork.CompleteAsync();

        return new CreatePlanCommandResponse(plan.Id);
    }
}
