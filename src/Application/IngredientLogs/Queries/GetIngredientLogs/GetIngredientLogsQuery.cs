using ErrorOr;
using MediatR;

namespace Application.IngredientLogs.Queries.GetIngredientLogs;

public record GetIngredientLogsQuery(
    int? IngredientId,
    bool? OrderDateDesc,
    int? PageNumber,
    int? PageSize
) : IRequest<ErrorOr<GetIngredientLogsResponse>>;

public record GetIngredientLogsResponse(
    List<IngredientLogMinimum> IngredientLogs,
    int TotalCount,
    int PageNumber,
    int PageSize,
    int TotalPages,
    bool HasPreviousPage,
    bool HasNextPage
);
public record IngredientLogMinimum(
    int IngredientId,
    string Name,
    DateTime Date,
    int Quantity
);