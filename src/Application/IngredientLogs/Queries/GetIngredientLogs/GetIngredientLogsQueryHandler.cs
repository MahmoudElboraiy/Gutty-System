using Application.Interfaces.UnitOfWorkInterfaces;
using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.IngredientLogs.Queries.GetIngredientLogs;

public class GetIngredientLogsQueryHandler
    : IRequestHandler<GetIngredientLogsQuery, ErrorOr<GetIngredientLogsResponse>>
{
    private readonly IIngredientLogRepository _ingredientLogRepository;

    public GetIngredientLogsQueryHandler(IIngredientLogRepository ingredientLogRepository)
    {
        _ingredientLogRepository = ingredientLogRepository;
    }

    public async Task<ErrorOr<GetIngredientLogsResponse>> Handle(
        GetIngredientLogsQuery request,
        CancellationToken cancellationToken
    )
    {
        var ingredientLogQueryable = _ingredientLogRepository.GetAll();

        var totalCount = ingredientLogQueryable.Count();

        var pageSize = request.PageSize ?? 20;
        var pageNumber = request.PageNumber ?? 1;

        var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

        var hasNextPage = pageNumber < totalPages;

        var hasPreviousPage = pageNumber > 1;

        if (request.OrderDateDesc == true)
        {
            ingredientLogQueryable = ingredientLogQueryable.OrderByDescending(i => i.Date);
        }
        else
        {
            ingredientLogQueryable = ingredientLogQueryable.OrderBy(i => i.Date);
        }

        if (request.IngredientId != null)
        {
            ingredientLogQueryable = ingredientLogQueryable.Where(i =>
                i.IngredientId == request.IngredientId
            );
        }

        var ingredientLogs = await ingredientLogQueryable
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Include(i => i.Ingredient)
            .ToListAsync(cancellationToken: cancellationToken);

        var finalResult = ingredientLogs
            .Select(item => new IngredientLogMinimum(
                item.Ingredient.Id,
                item.Ingredient.Name,
                item.Date,
                item.Quantity
            ))
            .ToList();

        return new GetIngredientLogsResponse(
            finalResult,
            totalCount,
            pageNumber,
            pageSize,
            totalPages,
            hasPreviousPage,
            hasNextPage
        );
    }
}
