
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Meals.Command.UpdateSubCategory;

public record UpdateSubCategoryCommand(
    int SubCategoryId,
    string SubCategoryName,
    IFormFile? Image,
    int CategoryId
) : IRequest<ErrorOr<UpdateSubCategoryCommandResponse>>;
public record UpdateSubCategoryCommandResponse(
    int SubCategoryId
);
