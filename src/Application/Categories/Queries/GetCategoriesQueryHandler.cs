
using Application.Cache;
using Application.Interfaces;
using Application.Interfaces.UnitOfWorkInterfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Categories.Queries;

public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, GetCategoriesQueryResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICacheService _cacheService;
    public GetCategoriesQueryHandler(IUnitOfWork unitOfWork, ICacheService cacheService)
    {
        _unitOfWork = unitOfWork;
        _cacheService = cacheService;
    }
    public async Task<GetCategoriesQueryResponse> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        string parametersKey = "all";
        var response = await _cacheService.GetOrCreateAsync(
           baseKey: CacheKeys.CategoriesAll,
           versionKey: CacheKeys.CategoriesVersion,
           parametersKey: parametersKey,
           factory: async () =>
           {
               var categories = await _unitOfWork.Categories
            .GetQueryable()
            .AsNoTracking()
            .ToListAsync(cancellationToken);
        var responseItems = categories.Select(c => new GetCategoryQueryResponseItem(c.Id, c.Name,c.MealType)).ToList();
               return new GetCategoriesQueryResponse(responseItems);
           });
        return response;
    }
}
