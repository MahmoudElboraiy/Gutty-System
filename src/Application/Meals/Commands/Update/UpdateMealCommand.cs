using Domain.Enums;
using ErrorOr;
using MediatR;

namespace Application.Meals.Commands.UpdateMeal;

public record UpdateMealCommand(
    Guid Id,
    Guid ItemId,
    decimal Weight,
    decimal Price,
    MealType MealType,
    uint Quantity
) : IRequest<ErrorOr<UpdateMealCommandResponse>>;

public record UpdateMealCommandResponse(Guid Id);