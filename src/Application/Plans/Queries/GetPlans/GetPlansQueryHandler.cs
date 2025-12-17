using Application.Cache;
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
    private readonly IMemoryCache _cache;
    public GetPlansQueryHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, IMemoryCache memoryCache)
    {
        _unitOfWork = unitOfWork;
        _httpContextAccessor = httpContextAccessor;
        _cache = memoryCache;
    }

    public async Task<GetPlansQueryResponse> Handle(
        GetPlansQuery request,
        CancellationToken cancellationToken
    )
    {
        if (!_cache.TryGetValue(CacheKeys.Plans, out List<GetPlanQueryResponseItem> allPlans))
        {
             var Plans = await _unitOfWork
            .Plans.GetQueryable()
            .Include(p => p.LunchCategories) 
            .AsNoTracking()
            .ToListAsync(cancellationToken);

            var httpRequest = _httpContextAccessor.HttpContext!.Request;
            var baseUrl = $"{httpRequest.Scheme}://{httpRequest.Host}";

            allPlans = Plans.Select(x => x.MapPlanResponse(baseUrl)).ToList();

            _cache.Set(
                 CacheKeys.Plans,
                 allPlans,
                  new MemoryCacheEntryOptions
                  {
                      SlidingExpiration = TimeSpan.FromHours(24),
                      Priority = CacheItemPriority.High
                  }
                        );      
        }

        var items = allPlans
           .Skip((request.pageNumber - 1) * request.pageSize)
           .Take(request.pageSize)
           .ToList();

        return new GetPlansQueryResponse(items);
    }
} 