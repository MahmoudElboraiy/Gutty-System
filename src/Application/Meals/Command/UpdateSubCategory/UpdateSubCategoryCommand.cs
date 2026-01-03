
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Meals.Command.UpdateSubCategory;

public record UpdateSubCategoryCommand(
    int SubCategoryId,
    string SubCategoryName,
    int CategoryId,
    IFormFile? Image=null
) : IRequest<ErrorOr<UpdateSubCategoryCommandResponse>>;
public record UpdateSubCategoryCommandResponse(
    int SubCategoryId
);
