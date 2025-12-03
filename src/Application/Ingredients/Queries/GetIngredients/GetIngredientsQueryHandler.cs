

using Application.Interfaces.UnitOfWorkInterfaces;
using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Ingredients.Queries.GetIngredients;

public class GetIngredientsQueryHandler : IRequestHandler<GetIngredientsQuery, ErrorOr<List<GetIngredientsQueryResponse>>>
{
    private readonly IUnitOfWork _unitOfWork;
    public GetIngredientsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<ErrorOr<List<GetIngredientsQueryResponse>>> Handle(GetIngredientsQuery request, CancellationToken cancellationToken)
    {
        var skip = (request.pageNumber - 1) * request.pageSize;
        var ingredients = await _unitOfWork.Ingredients
            .GetQueryable()
            .AsNoTracking()
            .Skip(skip)
            .Take(request.pageSize)
            .ToListAsync(cancellationToken);
        var response = ingredients.Select(ingredient => new GetIngredientsQueryResponse(
            ingredient.Id,
            ingredient.Name,
            ingredient.CaloriesPer100g,
            ingredient.ProteinPer100g,
            ingredient.CarbsPer100g,
            ingredient.FatsPer100g
            )).ToList();
        return response;
    }
}
