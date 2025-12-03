

using ErrorOr;
using MediatR;

namespace Application.Meals.Command.DeleteMeal;

public record DeleteMealCommand(
    int Id
) : IRequest<ErrorOr<ResultMessage>>;
public record ResultMessage
{
    public string Message { get; set; }
    public bool Success { get; set; }
}
