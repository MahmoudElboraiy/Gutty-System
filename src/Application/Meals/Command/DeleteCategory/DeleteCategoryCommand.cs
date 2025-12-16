

using ErrorOr;
using MediatR;

namespace Application.Meals.Command.DeleteCategory;

public record DeleteCategoryCommand(
    int Id
) : IRequest<ErrorOr<DeleteCategoryCommandResponse>>;
public record DeleteCategoryCommandResponse(
    int Id
);

