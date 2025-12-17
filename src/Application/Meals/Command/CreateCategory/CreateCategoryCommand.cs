

using Domain.Enums;
using Domain.Models.Entities;
using ErrorOr;
using MediatR;

namespace Application.Meals.Command.CreateCategory;

public record CreateCategoryCommand(
    string Name,
    MealType MealType
) : IRequest<ErrorOr<CreateCategoryCommandResponse>>;
public record CreateCategoryCommandResponse(
    int Id
);




