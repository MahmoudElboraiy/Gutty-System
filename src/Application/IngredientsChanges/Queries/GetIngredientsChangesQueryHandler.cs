using Application.Interfaces.UnitOfWorkInterfaces;
using Application.Profiles;
using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.IngredientsChanges.Queries;

public class GetIngredientsChangesQueryHandler
    : IRequestHandler<GetIngredientsChangesQuery, ErrorOr<GetIngredientsChangesQueryResponse>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetIngredientsChangesQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<GetIngredientsChangesQueryResponse>> Handle(
        GetIngredientsChangesQuery request,
        CancellationToken cancellationToken
    )
    {
        int page = request.Page ?? 1;
        int pageSize = request.PageSize ?? 10;

        var changes = _unitOfWork.IngredientChanges.GetQueryable();

        var count = changes.Count();
        var totalPages = (int)Math.Ceiling(count / (double)pageSize);
        var currentPage = page > totalPages ? totalPages : page;
        var hasNextPage = currentPage < totalPages;
        var hasPreviousPage = currentPage > 1;

        var changesToReturn = changes.Skip((currentPage - 1) * pageSize).Take(pageSize);

        if (request.IngredientId != null)
        {
            changesToReturn = changesToReturn.Where(c => c.IngredientId == request.IngredientId);
        }
        else if (request.Name != null)
        {
            changesToReturn = changesToReturn.Where(c => c.Ingredient.Name.Contains(request.Name));
        }

        if (request.ChangeDate != null)
        {
            changesToReturn = changesToReturn.Where(c =>
                c.CreatedAt.Date == request.ChangeDate.Value.Date
            );
        }

        if (request.IsDescending == true)
        {
            changesToReturn = changesToReturn.OrderByDescending(c => c.CreatedAt);
        }

        var Items = await changesToReturn
            .Include(c => c.Ingredient)
            .Select(c => c.MapIngredientChangeResponse())
            .ToListAsync(cancellationToken);

        return new GetIngredientsChangesQueryResponse(
            page,
            pageSize,
            totalPages,
            count,
            hasPreviousPage,
            hasNextPage,
            Items
        );
    }
}
