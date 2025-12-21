
using Application.Cache;
using Application.Interfaces;
using Application.Interfaces.UnitOfWorkInterfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Application.SubCategories.Query.GetSubCategories;

public class GetSubCategoriesQueryHandler : IRequestHandler<GetSubCategoriesQuery, GetSubCategoriesQueryResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ICacheService _cacheService;
    public GetSubCategoriesQueryHandler(IUnitOfWork unitOfWork,IHttpContextAccessor httpContextAccessor,ICacheService cacheService)
    {
        _unitOfWork = unitOfWork;
        _httpContextAccessor = httpContextAccessor;
        _cacheService = cacheService;
    }
    public async Task<GetSubCategoriesQueryResponse> Handle(
        GetSubCategoriesQuery request,
        CancellationToken cancellationToken
    )
    {
        var parametersKey = $"category_{request.CategoryId}";
        return await _cacheService.GetOrCreateAsync<
            GetSubCategoriesQueryResponse>(
            baseKey: CacheKeys.SubCategories,
            versionKey: CacheKeys.SubCategoriesVersion,
            parametersKey: parametersKey,
            factory: async () =>
            {
                var subCategories = await _unitOfWork.SubCategories
                    .GetQueryable()
                    .Where(sc => sc.CategoryId == request.CategoryId)
                    .AsNoTracking()
                    .ToListAsync(cancellationToken);

                var httpRequest = _httpContextAccessor.HttpContext!.Request;
                var baseUrl = $"https://{httpRequest.Host}";

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
            });
    }
}
