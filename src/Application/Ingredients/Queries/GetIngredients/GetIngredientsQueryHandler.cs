

using Application.Interfaces.UnitOfWorkInterfaces;
using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Ingredients.Queries.GetIngredients;

public class GetIngredientsQueryHandler : IRequestHandler<GetIngredientsQuery, ErrorOr<GetIngredientsQueryResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    public GetIngredientsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<ErrorOr<GetIngredientsQueryResponse>> Handle(GetIngredientsQuery request, CancellationToken cancellationToken)
    {
        var query = _unitOfWork.Ingredients
       .GetQueryable()
       .AsNoTracking();
        if (!string.IsNullOrWhiteSpace(request.searchName))
        {
            string search = request.searchName.ToLower();

            query = query.Where(i => i.Name.ToLower().Contains(search));

        }
        var totalCount = await query.CountAsync(cancellationToken);

        var skip = (request.pageNumber - 1) * request.pageSize;

        var ingredients = await query
            .OrderBy(i => i.Name)
            .Skip(skip)
            .Take(request.pageSize)
            .ToListAsync(cancellationToken);

    
        var response = ingredients.Select(ingredient => new GetIngredientsItem(
            ingredient.Id,
            ingredient.Name,
            ingredient.CaloriesPer100g,
            ingredient.ProteinPer100g,
            ingredient.CarbsPer100g,
            ingredient.FatsPer100g
        )).ToList();

        var finalResponse = new GetIngredientsQueryResponse(
            request.pageNumber,
            request.pageSize,
            totalCount,
            response
        );
        return finalResponse;
    }
}
