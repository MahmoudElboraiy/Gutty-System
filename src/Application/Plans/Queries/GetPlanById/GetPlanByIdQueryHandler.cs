

using Application.Cache;
using Application.Interfaces.UnitOfWorkInterfaces;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Application.Plans.Queries.GetPlanById;

public class GetPlanByIdQueryHandler : IRequestHandler<GetPlanByIdQuery, ErrorOr<GetPlanByIdQueryResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMemoryCache _cache;
    public GetPlanByIdQueryHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor
        , IMemoryCache memoryCache)
    {
        _unitOfWork = unitOfWork;
        _httpContextAccessor = httpContextAccessor;
        _cache = memoryCache;
    }
    public async Task<ErrorOr<GetPlanByIdQueryResponse>> Handle(GetPlanByIdQuery request, CancellationToken cancellationToken)
    {


        var plan = await _unitOfWork.Plans
            .GetQueryable()
            .Include(p => p.LunchCategories)
            .AsNoTracking()
            .Where(p =>p.Id ==request.PlanId)
            .FirstOrDefaultAsync(cancellationToken);

        if (plan == null)
        {
            return Error.NotFound(description: "Plan not found");
        }

        var httpRequest = _httpContextAccessor.HttpContext!.Request;
        var baseUrl = $"{httpRequest.Scheme}://{httpRequest.Host}";

        var categories = plan.LunchCategories.Select(c => new GetPlanCategoryByIdResponseItem(
            Id: c.Id,
            Name: c.Name,
            NumberOfMeals: c.NumberOfMeals,
            ProteinGrams: c.ProteinGrams,
            PricePerGram: c.PricePerGram,
            AllowProteinChange: c.AllowProteinChange,
            MaxProteinGrams: c.MaxProteinGrams,
            CategoryPrice: c.GetCategoryPrice()
        )).ToList();

        var response = new GetPlanByIdQueryResponse(
            Id: plan.Id,
            Name: plan.Name,
            Description: plan.Description,
            ImageUrl: baseUrl + plan.ImageUrl,
            DurationInDays: plan.DurationInDays,
            LMealsPerDay: plan.LMealsPerDay,
            BDMealsPerDay: plan.BDMealsPerDay,
            BreakfastPrice: plan.BreakfastPrice,
            DinnerPrice: plan.DinnerPrice,
            TotalPrice: plan.GetTotalPrice(),
            CarbGrams: plan.CarbGrams,
            MaxCarbGrams: plan.MaxCarbGrams,
            Categories: categories
        );

        return response;
    }
}
