using Application.Interfaces.UnitOfWorkInterfaces;
using Application.Items.Queries.GetItem;
using Application.Profiles;
using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Items.Queries.GetItems;

public class GetItemsQueryHandler : IRequestHandler<GetItemsQuery, ErrorOr<GetItemsQueryResponse>>
{
    private readonly IItemRepository _itemRepository;

    public GetItemsQueryHandler(IItemRepository itemRepository)
    {
        _itemRepository = itemRepository;
    }

    public async Task<ErrorOr<GetItemsQueryResponse>> Handle(
        GetItemsQuery request,
        CancellationToken cancellationToken
    )
    {
        var items = await _itemRepository
            .GetQueryable()
            .AsNoTracking()
            .Include(i => i.RecipeIngredients)
            .Where(x =>
                x.Name.Contains(request.SearchText ?? "")
                || x.Description.Contains(request.SearchText ?? "")
            )
            .ToListAsync(cancellationToken: cancellationToken);

        var result = new GetItemsQueryResponse(items.Select(x => x.MapItemResponse()).ToList());

        return result;
    }
}
