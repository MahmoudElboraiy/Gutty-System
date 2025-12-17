using Application.Interfaces.UnitOfWorkInterfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Application.Meals.Query.GetMeals;

public class GetMealsQueryHandler : IRequestHandler<GetMealsQuery, GetMealsQueryResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public GetMealsQueryHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _httpContextAccessor = httpContextAccessor;
    }
    public async Task<GetMealsQueryResponse> Handle(GetMealsQuery request, CancellationToken cancellationToken)
    {
        var meals = await _unitOfWork.Meals
            .GetQueryable()
            .Where(m => m.SubcategoryId == request.SubCategoryId)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        var httpRequest = _httpContextAccessor.HttpContext!.Request;
        var baseUrl = $"{httpRequest.Scheme}://{httpRequest.Host}";

        var responseItems = meals
            .Select(
                m =>
                    new GetMealQueryResponseItem(
                        m.Id,
                        m.Name,
                        m.Description,
                        baseUrl + m.ImageUrl
                    )
            )
            .ToList();
        return new GetMealsQueryResponse(responseItems);
    }
}
