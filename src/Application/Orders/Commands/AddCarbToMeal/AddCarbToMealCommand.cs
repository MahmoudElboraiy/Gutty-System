
using MediatR;

namespace Application.Orders.Commands.AddCarbToMeal;

public record AddCarbToMealCommand(int orderMealId, int CarbId) : IRequest<AddCarbToMealCommandResponse>;
public record AddCarbToMealCommandResponse(bool success, string message);
