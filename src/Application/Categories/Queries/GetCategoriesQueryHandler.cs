
using Application.Interfaces.UnitOfWorkInterfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Categories.Queries;

public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, GetCategoriesQueryResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    public GetCategoriesQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<GetCategoriesQueryResponse> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await _unitOfWork.Categories
            .GetQueryable()
            .AsNoTracking()
            .ToListAsync(cancellationToken);
        var responseItems = categories.Select(c => new GetCategoryQueryResponseItem(c.Id, c.Name)).ToList();
        return new GetCategoriesQueryResponse(responseItems);
    }
}
