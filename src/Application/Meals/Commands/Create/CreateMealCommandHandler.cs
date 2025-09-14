using ErrorOr;
using MediatR;
using Application.Interfaces.UnitOfWorkInterfaces;
using Domain.DErrors;
using Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Meals.Commands.Create;

public class CreateMealCommandHandler : IRequestHandler<CreateMealCommand, ErrorOr<CreateMealCommandResponse>>
{
    public readonly IUnitOfWork _unitOfWork;
    public CreateMealCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<ErrorOr<CreateMealCommandResponse>> Handle(CreateMealCommand request, CancellationToken cancellationToken)
    {
        var item = await _unitOfWork.Items.GetByIdAsync(request.ItemId);
        if (item == null)
        {
            return DomainErrors.Items.ItemNotFound(request.ItemId);
        }

        var meal = new Meal
        {
            Id = Guid.NewGuid(),
            ItemId = request.ItemId,
            Weight = request.Weight,
            Price = request.Price,
            MealType = request.MealType,
            Quantity = request.Quantity
        };
        await _unitOfWork.Meals.AddAsync(meal);
        await _unitOfWork.CompleteAsync();
        return new CreateMealCommandResponse(meal.Id);
    }
}
