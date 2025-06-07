using Application.Interfaces.UnitOfWorkInterfaces;
using Application.Items.Queries.GetItem;
using Application.Profiles;
using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Items.Queries.GetItems;

public class GetItemsQueryHandler : IRequestHandler<GetItemsQuery, ErrorOr<GetItemsQueryResponse>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetItemsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<GetItemsQueryResponse>> Handle(
        GetItemsQuery request,
        CancellationToken cancellationToken
    )
    {
        var items = await _unitOfWork
            .Items.GetQueryable()
            .AsNoTracking()
            .Include(i => i.Ingredients)
            .Where(x =>
                x.Name.Contains(request.SearchText ?? "")
                || x.NameAr.Contains(request.SearchText ?? "")
                || x.Description.Contains(request.SearchText ?? "")
                || x.DescriptionAr.Contains(request.SearchText ?? "")
            )
            .ToListAsync(cancellationToken: cancellationToken);

        var result = new GetItemsQueryResponse(items.Select(x => x.MapItemResponse()).ToList());

        return result;
    }
}
