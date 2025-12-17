
using Application.Interfaces.UnitOfWorkInterfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Application.SubCategories.Query.GetSubCategories;

public class GetSubCategoriesQueryHandler : IRequestHandler<GetSubCategoriesQuery, GetSubCategoriesQueryResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public GetSubCategoriesQueryHandler(IUnitOfWork unitOfWork,IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _httpContextAccessor = httpContextAccessor;
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

        var httpRequest = _httpContextAccessor.HttpContext!.Request;
        var baseUrl = $"{httpRequest.Scheme}://{httpRequest.Host}";

        var responseItems = subCategories
            .Select(
                sc =>
                    new GetSubCategoryQueryResponseItem(
                        sc.Id,
                        sc.Name,
                        sc.ImageUrl = baseUrl + sc.ImageUrl,
                        sc.CategoryId
                    )
            )
            .ToList();
        return new GetSubCategoriesQueryResponse(responseItems);
    }
}
