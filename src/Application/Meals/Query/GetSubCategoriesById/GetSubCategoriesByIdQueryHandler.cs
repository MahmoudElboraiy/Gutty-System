
using Application.Interfaces.UnitOfWorkInterfaces;
using ErrorOr;
using MediatR;

namespace Application.Meals.Query.GetSubCategoriesById;

public class GetSubCategoriesByIdQueryHandler:
    IRequestHandler<GetSubCategoriesByIdQuery, ErrorOr<GetSubCategoriesByIdQueryResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    public GetSubCategoriesByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<ErrorOr<GetSubCategoriesByIdQueryResponse>> Handle(GetSubCategoriesByIdQuery request, CancellationToken cancellationToken)
    {
        var subCategory = await _unitOfWork.SubCategories.GetByIdAsync(request.SubCategoryId);
        if (subCategory == null)
        {
            return Error.NotFound("SubCategory.NotFound", $"SubCategory with Id {request.SubCategoryId} not found.");
        }
        return new GetSubCategoriesByIdQueryResponse(
            subCategory.Id,
            subCategory.Name,
            subCategory.ImageUrl,
            subCategory.CategoryId
        );
    }
}
