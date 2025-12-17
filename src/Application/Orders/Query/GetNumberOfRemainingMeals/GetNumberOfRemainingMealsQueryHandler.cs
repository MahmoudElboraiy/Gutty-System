

using Application.Interfaces;
using Application.Interfaces.UnitOfWorkInterfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Application.Orders.Query.GetNumberOfRemainingMeals;

public class GetNumberOfRemainingMealsQueryHandler : IRequestHandler<GetNumberOfRemainingMealsQuery, GetNumberOfRemainingMealsQueryResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;
    public GetNumberOfRemainingMealsQueryHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
    {
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
    }
    public async Task<GetNumberOfRemainingMealsQueryResponse> Handle(GetNumberOfRemainingMealsQuery request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;


           var remainingMeals = await _unitOfWork.Subscriptions.GetQueryable()
            .Where(s => s.UserId == userId && s.IsCurrent && !s.IsPaused)
            .SelectMany(s => s.LunchCategories
            .Select(sc => new RemainingMealsItem(sc.SubCategoryId ?? 0, (int)sc.NumberOfMealsLeft))
            ).ToListAsync(cancellationToken);

        return new GetNumberOfRemainingMealsQueryResponse(remainingMeals);
    }
}
