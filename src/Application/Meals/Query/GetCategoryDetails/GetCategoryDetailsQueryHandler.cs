

using Application.Cache;
using Application.Interfaces;
using Application.Interfaces.UnitOfWorkInterfaces;
using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Meals.Query.GetCategoryDetails;

public class GetCategoryDetailsQueryHandler :
    IRequestHandler<GetCategoryDetailsQuery, ErrorOr<GetCategoryDetailsQueryResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICacheService _cacheService;
    public GetCategoryDetailsQueryHandler(IUnitOfWork unitOfWork, ICacheService cacheService)
    {
        _unitOfWork = unitOfWork;
        _cacheService = cacheService;
    }
    public async Task<ErrorOr<GetCategoryDetailsQueryResponse>> Handle(GetCategoryDetailsQuery request, CancellationToken cancellationToken)
    {
        var cacheKeyParams = $"category_{request.Id}";

        var response = await _cacheService.GetOrCreateAsync<
            GetCategoryDetailsQueryResponse>(
            baseKey: CacheKeys.CategoryById,
            versionKey: CacheKeys.CategoriesVersion,
            parametersKey: cacheKeyParams,
            factory: async () =>
            {
                return await _unitOfWork.Categories
                    .GetQueryable()
                    .AsNoTracking()
                    .Where(c => c.Id == request.Id)
                    .Select(c => new GetCategoryDetailsQueryResponse(
                        c.Id,
                        c.Name,
                        c.MealType
                    ))
                    .FirstOrDefaultAsync(cancellationToken);
            });

        if (response is null)
        {
            return Error.NotFound(
                "Category.NotFound",
                $"Category with Id {request.Id} not found."
            );
        }

        return response;
    }
}
