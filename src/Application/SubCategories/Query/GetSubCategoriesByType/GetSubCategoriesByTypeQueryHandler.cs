

using Application.Interfaces.UnitOfWorkInterfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.SubCategories.Query.GetBreakFastAndDinner;

public  class GetSubCategoriesByTypeQueryHandler : IRequestHandler<GetSubCategoriesByTypeQuery, GetSubCategoriesByTypeQueryResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    public GetSubCategoriesByTypeQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<GetSubCategoriesByTypeQueryResponse> Handle(GetSubCategoriesByTypeQuery request, CancellationToken cancellationToken)
    {
        var breakFastAndDinnerMeals = await _unitOfWork.SubCategories.GetQueryable()
            .AsNoTracking()
            .Where(m => m.Category.MealType == request.MealType)
            .Select(n =>
            new
            {
                n.Id,
                n.Name,
                n.CategoryId
            })
            .ToListAsync(cancellationToken);

       var responseItems = breakFastAndDinnerMeals
            .Select(m => new GetSubCategoriesByTypeQueryResponseItem(
                m.Id,
                m.Name,
                m.CategoryId
                ))
            .ToList();
        return new GetSubCategoriesByTypeQueryResponse(responseItems);
    }
}
