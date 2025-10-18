
using MediatR;

namespace Application.Orders.Commands.AddMeal;

public record AddMealCommand(int MealId,int count, string? Notes):IRequest<AddMealCommandResponse>;

public record AddMealCommandResponse(bool success, string message);