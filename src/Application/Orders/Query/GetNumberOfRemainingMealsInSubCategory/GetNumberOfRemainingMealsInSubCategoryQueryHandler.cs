
using Application.Interfaces;
using Application.Interfaces.UnitOfWorkInterfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Orders.Query.GetNumberOfRemainingMealsInSubCategory;

public class GetNumberOfRemainingMealsInSubCategoryQueryHandler:IRequestHandler<GetNumberOfRemainingMealsInSubCategoryQuery, GetNumberOfRemainingMealsInSubCategoryQueryResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;
    public GetNumberOfRemainingMealsInSubCategoryQueryHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
    {
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
    }
    public async Task<GetNumberOfRemainingMealsInSubCategoryQueryResponse> Handle(GetNumberOfRemainingMealsInSubCategoryQuery request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;
        var remainingMealsInSubCategory = await _unitOfWork.Subscriptions.GetQueryable()
             .Where(s => s.UserId == userId && s.IsCurrent && !s.IsPaused)
             .SelectMany(s => s.LunchCategories
             .Where(sc => sc.SubCategoryId == request.SubCategoryId)
             .Select(sc => (int)sc.NumberOfMealsLeft)
             ).FirstOrDefaultAsync(cancellationToken);
        return new GetNumberOfRemainingMealsInSubCategoryQueryResponse(remainingMealsInSubCategory);
    }
}
