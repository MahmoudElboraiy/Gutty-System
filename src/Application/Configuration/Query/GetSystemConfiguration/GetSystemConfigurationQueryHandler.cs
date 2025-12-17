
using Application.Interfaces.UnitOfWorkInterfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Configuration.Query.GetSystemConfiguration;

public class GetSystemConfigurationQueryHandler : IRequestHandler<GetSystemConfigurationQuery, GetSystemConfigurationQueryResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetSystemConfigurationQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<GetSystemConfigurationQueryResponse> Handle(GetSystemConfigurationQuery request, CancellationToken cancellationToken)
    {
        var config = await _unitOfWork
            .Configurations
            .GetQueryable()
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken);
        if(config == null)
        {
            return new GetSystemConfigurationQueryResponse(0,0,0);
        }
        return new GetSystemConfigurationQueryResponse(config.DailyCapacity,config.MinimumDaysToOrder,config.MaximumDaysToOrder);
    }
}
