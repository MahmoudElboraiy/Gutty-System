
using MediatR;

namespace Application.Orders.Commands.RemoveCarbFromMeal;

public record RemoveCarbFromMealCommand(int orderMealId) : IRequest<RemoveCarbFromMealCommandResponse>;
public record RemoveCarbFromMealCommandResponse(bool success, string message);
