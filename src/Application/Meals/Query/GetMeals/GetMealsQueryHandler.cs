using Application.Interfaces.UnitOfWorkInterfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Meals.Query.GetMeals;

public class GetMealsQueryHandler : IRequestHandler<GetMealsQuery, GetMealsQueryResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    public GetMealsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<GetMealsQueryResponse> Handle(GetMealsQuery request, CancellationToken cancellationToken)
    {
        var meals = await _unitOfWork.Meals
            .GetQueryable()
            .Where(m => m.SubcategoryId == request.SubCategoryId)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
        var responseItems = meals
            .Select(
                m =>
                    new GetMealQueryResponseItem(
                        m.Id,
                        m.Name,
                        m.Description,
                        m.ImageUrl
                    )
            )
            .ToList();
        return new GetMealsQueryResponse(responseItems);
    }
}
