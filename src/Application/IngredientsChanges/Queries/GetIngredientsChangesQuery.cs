using ErrorOr;
using MediatR;

namespace Application.IngredientsChanges.Queries;

public record GetIngredientsChangesQuery(
    int? IngredientId,
    string? Name,
    DateTime? ChangeDate,
    int? Page,
    int? PageSize,
    bool? IsDescending
) : IRequest<ErrorOr<GetIngredientsChangesQueryResponse>>;

public record GetIngredientsChangesQueryResponse(
    int Page,
    int PageSize,
    int TotalPages,
    int TotalCount,
    bool HasPrevious,
    bool HasNext,
    List<GetIngredientsChangesQueryResponseItem> Items
);

public record GetIngredientsChangesQueryResponseItem(
    DateTime ChangeDate,
    decimal Quantity,
    decimal OldValue,
    decimal NewValue,
    GetIngredientsChangesResponseIngredientQuery Ingredient
);

public record GetIngredientsChangesResponseIngredientQuery(int Id, string Name);
