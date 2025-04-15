using Application.Interfaces.UnitOfWorkInterfaces;
using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Ingredients.Queries.GetIngredients;

public class GetIngredientsQueryHandler : IRequestHandler<GetIngredientsQuery, ErrorOr<GetIngredientsQueryResponse>>
{
    private readonly IIngredientRepository _ingredientRepository;

    public GetIngredientsQueryHandler(IIngredientRepository ingredientRepository)
    {
        _ingredientRepository = ingredientRepository;
    }

    public async Task<ErrorOr<GetIngredientsQueryResponse>> Handle(GetIngredientsQuery request, CancellationToken cancellationToken)
    {
        var ingredientsQ = _ingredientRepository.GetAllQueryable();

        var ingredients = await ingredientsQ.AsNoTracking().Where(i => i.Name.Contains(request.SearchTerm ?? "")).ToListAsync(cancellationToken);
        
        return new GetIngredientsQueryResponse(ingredients);
    }
}