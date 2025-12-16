

using Application.Interfaces.UnitOfWorkInterfaces;
using ErrorOr;
using MediatR;

namespace Application.Meals.Query.GetCategoryDetails;

public class GetCategoryDetailsQueryHandler :
    IRequestHandler<GetCategoryDetailsQuery, ErrorOr<GetCategoryDetailsQueryResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    public GetCategoryDetailsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<ErrorOr<GetCategoryDetailsQueryResponse>> Handle(GetCategoryDetailsQuery request, CancellationToken cancellationToken)
    {
        var category = await _unitOfWork.Categories.GetByIdAsync(request.Id);
        if (category == null)
        {
            return Error.NotFound("Category.NotFound", $"Category with Id {request.Id} not found.");
        }
        return new GetCategoryDetailsQueryResponse(
            category.Id,
            category.Name,
            category.MealType
        );
    }
}
