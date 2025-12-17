

using ErrorOr;
using MediatR;

namespace Application.Meals.Command.DeleteSubCategory;

public  record DeleteSubCategoryCommand(
    int SubCategoryId
) : IRequest<ErrorOr<DeleteSubCategoryCommandResponse>>;
public record DeleteSubCategoryCommandResponse(
    int SubCategoryId
);
