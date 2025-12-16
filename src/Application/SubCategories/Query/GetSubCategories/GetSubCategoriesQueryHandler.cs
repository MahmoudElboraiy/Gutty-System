
using Application.Interfaces.UnitOfWorkInterfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.SubCategories.Query.GetSubCategories;

public class GetSubCategoriesQueryHandler : IRequestHandler<GetSubCategoriesQuery, GetSubCategoriesQueryResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    public GetSubCategoriesQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<GetSubCategoriesQueryResponse> Handle(
        GetSubCategoriesQuery request,
        CancellationToken cancellationToken
    )
    {
        var subCategories = await _unitOfWork.SubCategories
            .GetQueryable()
            .Where(sc => sc.CategoryId == request.CategoryId)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
        var responseItems = subCategories
            .Select(
                sc =>
                    new GetSubCategoryQueryResponseItem(
                        sc.Id,
                        sc.Name,
                        sc.ImageUrl,
                        sc.CategoryId
                    )
            )
            .ToList();
        return new GetSubCategoriesQueryResponse(responseItems);
    }
}
