
using Application.Cache;
using Application.Interfaces;
using Application.Interfaces.UnitOfWorkInterfaces;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Application.Meals.Query.GetSubCategoriesById;

public class GetSubCategoriesByIdQueryHandler:
    IRequestHandler<GetSubCategoriesByIdQuery, ErrorOr<GetSubCategoriesByIdQueryResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ICacheService _cacheService;
    public GetSubCategoriesByIdQueryHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, ICacheService cacheService)
    {
        _unitOfWork = unitOfWork;
        _httpContextAccessor = httpContextAccessor;
        _cacheService = cacheService;
    }
    public async Task<ErrorOr<GetSubCategoriesByIdQueryResponse>> Handle(GetSubCategoriesByIdQuery request, CancellationToken cancellationToken)
    {
        var httpRequest = _httpContextAccessor.HttpContext!.Request;
        var baseUrl = $"{httpRequest.Scheme}://{httpRequest.Host}";

        var cacheKeyParams = $"subCategory_{request.SubCategoryId}";

        var response = await _cacheService.GetOrCreateAsync<
            GetSubCategoriesByIdQueryResponse>(
            baseKey: CacheKeys.SubCategoryById,
            versionKey: CacheKeys.SubCategoriesVersion,
            parametersKey: cacheKeyParams,
            factory: async () =>
            {
                var subCategory = await _unitOfWork.SubCategories
                    .GetQueryable()
                    .AsNoTracking()
                    .Where(sc => sc.Id == request.SubCategoryId)
                    .Select(sc => new GetSubCategoriesByIdQueryResponse(
                        sc.Id,
                        sc.Name,
                        baseUrl + sc.ImageUrl,
                        sc.CategoryId
                    ))
                    .FirstOrDefaultAsync(cancellationToken);

                return subCategory!;
            });

        if (response is null)
        {
            return Error.NotFound(
                "SubCategory.NotFound",
                $"SubCategory with Id {request.SubCategoryId} not found."
            );
        }

        return response;
    }
}
