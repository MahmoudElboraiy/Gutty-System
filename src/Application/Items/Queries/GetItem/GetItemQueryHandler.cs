using Application.Interfaces.UnitOfWorkInterfaces;
using Application.Profiles;
using Domain.DErrors;
using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Items.Queries.GetItem;

public class GetItemQueryHandler : IRequestHandler<GetItemQuery, ErrorOr<GetItemQueryResponse>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetItemQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<GetItemQueryResponse>> Handle(
        GetItemQuery request,
        CancellationToken cancellationToken
    )
    {
        var item = await _unitOfWork
            .Items.GetQueryable()
            .AsNoTracking()
            .Include(i => i.Ingredients)
            .FirstOrDefaultAsync(i => i.Id == request.Id, cancellationToken: cancellationToken);

        if (item == null)
        {
            return DomainErrors.Items.ItemNotFound(request.Id);
        }

        return item.MapItemResponse();
    }
}
