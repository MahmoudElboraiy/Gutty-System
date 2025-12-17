

using Domain.Enums;
using ErrorOr;
using MediatR;

namespace Application.Meals.Command.UpdateCategory;

public record UpdateCategoryCommand(
    int Id,
    string Name,
    MealType MealType
) : IRequest<ErrorOr<UpdateCategoryCommandResponse>>;
public record UpdateCategoryCommandResponse(
    int Id
);
