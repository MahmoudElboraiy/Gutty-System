using Application.Interfaces.UnitOfWorkInterfaces;
using Application.Meals.Commands.DeleteMeal;
using Domain.DErrors;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Meals.Commands.Delete;

public class DeleteMealCommandHandler 
    : IRequestHandler<DeleteMealCommand, ErrorOr<DeleteMealCommandResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    public DeleteMealCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<ErrorOr<DeleteMealCommandResponse>> Handle(DeleteMealCommand request, CancellationToken cancellationToken)
    {
        var meal = await _unitOfWork.Meals.GetByIdAsync(request.Id);
        if (meal == null)
        {
            return DomainErrors.Meals.MealNotFound(request.Id);
        }
        _unitOfWork.Meals.Remove(meal);
        await _unitOfWork.CompleteAsync();
        return new DeleteMealCommandResponse(request.Id, true);
    }
}
