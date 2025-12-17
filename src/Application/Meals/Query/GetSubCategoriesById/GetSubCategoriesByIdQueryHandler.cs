
using Application.Interfaces.UnitOfWorkInterfaces;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Meals.Query.GetSubCategoriesById;

public class GetSubCategoriesByIdQueryHandler:
    IRequestHandler<GetSubCategoriesByIdQuery, ErrorOr<GetSubCategoriesByIdQueryResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public GetSubCategoriesByIdQueryHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _httpContextAccessor = httpContextAccessor;
    }
    public async Task<ErrorOr<GetSubCategoriesByIdQueryResponse>> Handle(GetSubCategoriesByIdQuery request, CancellationToken cancellationToken)
    {
        var subCategory = await _unitOfWork.SubCategories.GetByIdAsync(request.SubCategoryId);
        if (subCategory == null)
        {
            return Error.NotFound("SubCategory.NotFound", $"SubCategory with Id {request.SubCategoryId} not found.");
        }
        var httpRequest = _httpContextAccessor.HttpContext!.Request;
        var baseUrl = $"{httpRequest.Scheme}://{httpRequest.Host}";
        return new GetSubCategoriesByIdQueryResponse(
            subCategory.Id,
            subCategory.Name,
            baseUrl + subCategory.ImageUrl,
            subCategory.CategoryId
        );
    }
}
