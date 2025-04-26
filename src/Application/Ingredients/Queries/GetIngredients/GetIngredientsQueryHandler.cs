using Application.Interfaces.UnitOfWorkInterfaces;
using Application.Profiles;
using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Ingredients.Queries.GetIngredients;

public class GetIngredientsQueryHandler
    : IRequestHandler<GetIngredientsQuery, ErrorOr<GetIngredientsQueryResponse>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetIngredientsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<GetIngredientsQueryResponse>> Handle(
        GetIngredientsQuery request,
        CancellationToken cancellationToken
    )
    {
        var ingredientsQ = _unitOfWork.Ingredients.GetQueryable();

        var ingredients = await ingredientsQ
            .AsNoTracking()
            .Where(i => i.Name.Contains(request.SearchTerm ?? ""))
            .Select(x=> x.MapIngredientResponse())
            .ToListAsync(cancellationToken: cancellationToken);
        
        return new GetIngredientsQueryResponse(ingredients);
    }
}
