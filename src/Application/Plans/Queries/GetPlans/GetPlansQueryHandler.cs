using Application.Interfaces.UnitOfWorkInterfaces;
using Application.Profiles;
using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Plans.Queries.GetPlans;

public class GetPlansQueryHandler : IRequestHandler<GetPlansQuery, GetPlansQueryResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetPlansQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetPlansQueryResponse> Handle(
        GetPlansQuery request,
        CancellationToken cancellationToken
    )
    {
        var plans = await _unitOfWork
            .Plans.GetQueryable()
            .Include(p => p.LunchCategories) 
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        var result = new GetPlansQueryResponse(plans.Select(x => x.MapPlanResponse()).ToList());

        return result;
    }
} 