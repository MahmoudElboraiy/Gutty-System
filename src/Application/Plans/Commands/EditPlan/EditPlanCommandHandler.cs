

using Application.Interfaces.UnitOfWorkInterfaces;
using Domain.Models.Entities;
using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Plans.Commands.EditPlan;

public class EditPlanCommandHandler :
    IRequestHandler<EditPlanCommand, ErrorOr<EditPlanCommandResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    public EditPlanCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<ErrorOr<EditPlanCommandResponse>> Handle(EditPlanCommand request, CancellationToken cancellationToken)
    {
        var planExits = await _unitOfWork.Plans.GetQueryable()
            .Include(pc => pc.LunchCategories)
            .FirstOrDefaultAsync(p => p.Id == request.Id);
        if (planExits == null)
        {
            return Error.NotFound(description: "Plan not found");
        }
        planExits.Name = string.IsNullOrEmpty(request.Name) ? planExits.Name : request.Name;
        planExits.Description = string.IsNullOrEmpty(request.Description) ? planExits.Description : request.Description;
        planExits.ImageUrl = string.IsNullOrEmpty(request.ImageUrl) ? planExits.ImageUrl : request.ImageUrl;
        planExits.DurationInDays = (uint) request.DurationInDays;
        planExits.LMealsPerDay = request.LMealsPerDay;
        planExits.BDMealsPerDay = request.BDMealsPerDay;
        planExits.BreakfastPrice = request.BreakfastPrice;
        planExits.DinnerPrice = request.DinnerPrice;
        planExits.CarbGrams = (uint)request.CarbGrams;
        planExits.MaxCarbGrams = (uint)request.MaxCarbGrams;

        var oldCategories = planExits.LunchCategories.ToList();
        // Updating categories
        foreach (var newCategory in request.LunchCategories)
        {
            var existingCategory = oldCategories.FirstOrDefault(c => c.Id == newCategory.Id);

            if(existingCategory ==null)
            {
                return Error.NotFound(description: $"Category with Id {newCategory.Id} not found");
            }

            existingCategory.Name = newCategory.Name;
            existingCategory.NumberOfMeals = (uint)newCategory.NumberOfMeals;
            existingCategory.ProteinGrams = (uint)newCategory.ProteinGrams;
            existingCategory.PricePerGram = newCategory.PricePerGram;
            existingCategory.AllowProteinChange = newCategory.AllowProteinChange;
            existingCategory.MaxProteinGrams = (uint)newCategory.MaxProteinGrams;


        }
        
        _unitOfWork.Plans.Update(planExits);
        await _unitOfWork.CompleteAsync();
        return new EditPlanCommandResponse(planExits.Id);
    }
}
