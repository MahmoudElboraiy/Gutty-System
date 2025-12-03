
using Application.Interfaces.UnitOfWorkInterfaces;
using ErrorOr;
using MediatR;

namespace Application.Ingredients.Queries.GetIngredientById;

public class GetIngredientByIdQueryHandler :
    IRequestHandler<GetIngredientByIdQuery, ErrorOr<GetIngredientByIdQueryResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    public GetIngredientByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<ErrorOr<GetIngredientByIdQueryResponse>> Handle(GetIngredientByIdQuery request, CancellationToken cancellationToken)
    {
        var ingredient = await _unitOfWork.Ingredients.GetByIdAsync(request.Id);
        if (ingredient is null)
        {
            return Error.NotFound(description: $"Ingredient with id {request.Id} not found.");
        }
        var response = new GetIngredientByIdQueryResponse(
            ingredient.Id,
            ingredient.Name,
            ingredient.CaloriesPer100g,
            ingredient.ProteinPer100g,
            ingredient.CarbsPer100g,
            ingredient.FatsPer100g
            );
        return response;
    }
}
