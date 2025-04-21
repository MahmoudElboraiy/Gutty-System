using ErrorOr;
using MediatR;

namespace Application.Ingredients.Queries.GetIngredients;

public record GetIngredientsQuery(string? SearchTerm)
    : IRequest<ErrorOr<GetIngredientsQueryResponse>>;
