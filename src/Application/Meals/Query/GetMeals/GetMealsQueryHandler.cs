using Application.Cache;
using Application.Interfaces;
using Application.Interfaces.UnitOfWorkInterfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Application.Meals.Query.GetMeals;

public class GetMealsQueryHandler : IRequestHandler<GetMealsQuery, GetMealsQueryResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ICacheService _cacheService;
    public GetMealsQueryHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, ICacheService cacheService)
    {
        _unitOfWork = unitOfWork;
        _httpContextAccessor = httpContextAccessor;
        _cacheService = cacheService;
    }
    public async Task<GetMealsQueryResponse> Handle(GetMealsQuery request, CancellationToken cancellationToken)
    {
        var httpRequest = _httpContextAccessor.HttpContext!.Request;
        var baseUrl = $"{httpRequest.Scheme}://{httpRequest.Host}";

        var cacheKeyParams = $"subCategory_{request.SubCategoryId}";

        var responseItems = await _cacheService.GetOrCreateAsync<
            List<GetMealQueryResponseItem>>(
            baseKey: CacheKeys.Meals,
            versionKey: CacheKeys.MealsVersion,
            parametersKey: cacheKeyParams,
            factory: async () =>
            {
                return await _unitOfWork.Meals
                    .GetQueryable()
                    .AsNoTracking()
                    .Where(m => m.SubcategoryId == request.SubCategoryId)
                    .Select(m => new GetMealQueryResponseItem(
                        m.Id,
                        m.Name,
                        m.Description,
                        baseUrl + m.ImageUrl
                    ))
                    .ToListAsync(cancellationToken);
            });

        return new GetMealsQueryResponse(responseItems);
    }
}
