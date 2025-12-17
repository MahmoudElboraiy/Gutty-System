

using Application.Interfaces;
using Application.Interfaces.UnitOfWorkInterfaces;
using Domain.Models.Identity;
using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Subscriptions.Query.GetSubscriptionDetails;

public class GetSubscriptionDetailsQueryHandler : IRequestHandler<GetSubscriptionDetailsQuery, GetSubscriptionDetailsQueryResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;
    public GetSubscriptionDetailsQueryHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
    {
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
    }
    public async Task<GetSubscriptionDetailsQueryResponse> Handle(GetSubscriptionDetailsQuery request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;
        //var subscription = await _unitOfWork.Subscriptions.GetQueryable()
        //    .AsNoTracking()
        //    .Include(s => s.LunchCategories)
        //    .Where(s => s.UserId == userId && s.IsCurrent)
        //    .Select(subscription => new
        //    {
        //        subscription.Id,
        //        subscription.Plan.Name,
        //        subscription.IsPaused,
        //        subscription.StartDate,
        //        subscription.DaysLeft,
        //        subscription.LunchMealsLeft,
        //        subscription.CarbGrams,
        //        subscription.LunchCategories,
        //    })
        //    .FirstOrDefaultAsync(cancellationToken);
        //var LunchCategoriesDetails = subscription?.LunchCategories
        //    .Select(lc => new
        //    {
        //        lc.NumberOfMeals,
        //        lc.NumberOfMealsLeft,
        //        lc.ProteinGrams,
        //        lc.SubCategoryId,
        //    }).ToList();

        //var LunchMealsName = await _unitOfWork.SubCategories.GetQueryable()
        //    .Where(sc => LunchCategoriesDetails != null && LunchCategoriesDetails.Select(lc => lc.SubCategoryId).Contains(sc.Id))
        //    .Select(sc => new
        //    {
        //        sc.Name,
        //    }).ToListAsync(cancellationToken);
     var subscription = await _unitOfWork.Subscriptions.GetQueryable()
    .AsNoTracking()
    .Include(s => s.Plan)
    .Include(s => s.LunchCategories)
        .ThenInclude(lc => lc.Subcategory)
    .Where(s => s.UserId == userId && s.IsCurrent)
    .Select(s => new GetSubscriptionDetailsQueryResponse
    (
        true,
        s.Plan.Name,
        s.IsPaused,
        s.StartDate,
        s.DaysLeft,
        s.LunchMealsLeft,
        s.CarbGrams,
            s.LunchCategories.Select(lc => new LunchCategoryResponse(
            lc.Subcategory.Name,
            lc.NumberOfMeals,
            lc.NumberOfMealsLeft,
            lc.ProteinGrams
        )).ToList()
    ))
    .FirstOrDefaultAsync(cancellationToken);

        if (subscription == null)
        {
            return new GetSubscriptionDetailsQueryResponse(false, "", false, DateTime.MinValue, 0, 0, 0, new List<LunchCategoryResponse>());
        }
        return subscription;
    }
}
