
using Application.Cache;
using Application.Interfaces;
using Application.Interfaces.UnitOfWorkInterfaces;
using ErrorOr;
using MediatR;

namespace Application.Ingredients.Queries.GetIngredientById;

public class GetIngredientByIdQueryHandler :
    IRequestHandler<GetIngredientByIdQuery, ErrorOr<GetIngredientByIdQueryResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICacheService _cacheService;
    public GetIngredientByIdQueryHandler(IUnitOfWork unitOfWork,ICacheService cacheService)
    {
        _unitOfWork = unitOfWork;
        _cacheService = cacheService;
    }
    public async Task<ErrorOr<GetIngredientByIdQueryResponse>> Handle(GetIngredientByIdQuery request, CancellationToken cancellationToken)
    {
        string parametersKey = $"id_{request.Id}";
        var response1 = await _cacheService.GetOrCreateAsync < ErrorOr < GetIngredientByIdQueryResponse >> (
          baseKey: CacheKeys.IngredientById,
          versionKey: CacheKeys.IngredientsVersion,
          parametersKey: parametersKey,
          factory: async () =>
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
          });
        return response1;
     }
}
