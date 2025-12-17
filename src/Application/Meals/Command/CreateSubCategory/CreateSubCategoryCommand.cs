

using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Meals.Command.CreateSubCategory;

public record CreateSubCategoryCommand(
    string SubCategoryName,
    IFormFile Image,
    int CategoryId
) : IRequest<ErrorOr<CreateSubCategoryCommandResponse>>;
public record CreateSubCategoryCommandResponse(
    int SubCategoryId
);
