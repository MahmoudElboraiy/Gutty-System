using Application.Interfaces.UnitOfWorkInterfaces;
using Application.Profiles;
using Domain.DErrors;
using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Items.Queries.GetItem;

public class GetItemQueryHandler : IRequestHandler<GetItemQuery, ErrorOr<GetItemQueryResponse>>
{
    private readonly IItemRepository _itemRepository;
    private readonly IUnitOfWork _unitOfWork;

    public GetItemQueryHandler(IItemRepository itemRepository, IUnitOfWork unitOfWork)
    {
        _itemRepository = itemRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<GetItemQueryResponse>> Handle(GetItemQuery request, CancellationToken cancellationToken)
    {
        var item = await _itemRepository.GetQueryable().AsNoTracking().Include(i=>i.RecipeIngredients).
            FirstOrDefaultAsync(i=>i.Id == request.Id, cancellationToken: cancellationToken);

        if (item == null)
        {
            return DomainErrors.Items.ItemNotFound(request.Id);
        }

        return item.MapItemResponse();
    }
}