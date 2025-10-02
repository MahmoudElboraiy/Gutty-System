using MediatR;

namespace Application.Orders.Commands.RemoveMeal;

public record RemoveMealCommand(int orderMealId,int mealId) : IRequest<RemoveMealCommandResponse>;
public record RemoveMealCommandResponse(bool success, string message);
