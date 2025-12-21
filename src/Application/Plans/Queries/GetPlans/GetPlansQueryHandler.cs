using Application.Cache;
using Application.Interfaces;
using Application.Interfaces.UnitOfWorkInterfaces;
using Application.Profiles;
using Domain.Models.Entities;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Numerics;

namespace Application.Plans.Queries.GetPlans;

public class GetPlansQueryHandler : IRequestHandler<GetPlansQuery, GetPlansQueryResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ICacheService _cacheService;
    public GetPlansQueryHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, ICacheService cacheService)
    {
        _unitOfWork = unitOfWork;
        _httpContextAccessor = httpContextAccessor;
        _cacheService = cacheService;
    }

    public async Task<GetPlansQueryResponse> Handle(
        GetPlansQuery request,
        CancellationToken cancellationToken
    )
    {
        var httpRequest = _httpContextAccessor.HttpContext!.Request;
        var baseUrl = $"{httpRequest.Scheme}://{httpRequest.Host}";

        var parametersKey = $"page_{request.pageNumber}_size_{request.pageSize}";

        var pagePlans = await _cacheService.GetOrCreateAsync<List<GetPlanQueryResponseItem>>(
            baseKey: CacheKeys.Plans,
            versionKey: CacheKeys.PlansVersion,
            parametersKey: parametersKey,
            factory: async () =>
            {
                var skip = (request.pageNumber - 1) * request.pageSize;
                var take = request.pageSize;

                var plans = await _unitOfWork.Plans
                    .GetQueryable()
                    .Include(p => p.LunchCategories)
                    .AsNoTracking()
                    .OrderBy(p => p.Id)
                    .Skip(skip)
                    .Take(take)
                    .ToListAsync(cancellationToken);

                return plans.Select(p => p.MapPlanResponse(baseUrl)).ToList();
            });

        return new GetPlansQueryResponse(pagePlans);
    }
} 