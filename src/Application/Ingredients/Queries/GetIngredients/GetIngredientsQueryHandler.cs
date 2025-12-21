

using Application.Cache;
using Application.Interfaces;
using Application.Interfaces.UnitOfWorkInterfaces;
using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Application.Ingredients.Queries.GetIngredients;

public class GetIngredientsQueryHandler : IRequestHandler<GetIngredientsQuery, ErrorOr<GetIngredientsQueryResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICacheService _cacheService;
    public GetIngredientsQueryHandler(IUnitOfWork unitOfWork, ICacheService cacheService )
    {
        _unitOfWork = unitOfWork;
        _cacheService = cacheService;
    }
    public async Task<ErrorOr<GetIngredientsQueryResponse>> Handle(GetIngredientsQuery request, CancellationToken cancellationToken)
    {
        string parametersKey = $"search_{request.searchName}_pageNumber_{request.pageNumber}_pageSize_{request.pageSize}";
        var response = await _cacheService.GetOrCreateAsync(
           baseKey: CacheKeys.Ingredients,
           versionKey: CacheKeys.IngredientsVersion,
           parametersKey: parametersKey,
           factory: async () =>
           {
                var query = _unitOfWork.Ingredients
               .GetQueryable()
               .AsNoTracking();
                if (!string.IsNullOrWhiteSpace(request.searchName))
                {
                    string search = request.searchName.ToLower();

                    query = query.Where(i => i.Name.ToLower().Contains(search));

                }
                var totalCount = await query.CountAsync(cancellationToken);

                var skip = (request.pageNumber - 1) * request.pageSize;

                var ingredients = await query
                    .OrderBy(i => i.Name)
                    .Skip(skip)
                    .Take(request.pageSize)
                    .ToListAsync(cancellationToken);

    
                var response = ingredients.Select(ingredient => new GetIngredientsItem(
                    ingredient.Id,
                    ingredient.Name,
                    ingredient.CaloriesPer100g,
                    ingredient.ProteinPer100g,
                    ingredient.CarbsPer100g,
                    ingredient.FatsPer100g
                )).ToList();

                var finalResponse = new GetIngredientsQueryResponse(
                    request.pageNumber,
                    request.pageSize,
                    totalCount,
                    response
                );
                return finalResponse;
           });
        return response;
    }
}
