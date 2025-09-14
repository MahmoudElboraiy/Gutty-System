using Application.Meals.Commands.Delete;
using ErrorOr;
using MediatR;

namespace Application.Meals.Commands.DeleteMeal;

public record DeleteMealCommand(Guid Id) : IRequest<ErrorOr<DeleteMealCommandResponse>>;