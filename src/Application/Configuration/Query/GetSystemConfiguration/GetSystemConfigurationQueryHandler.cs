
using Application.Interfaces.UnitOfWorkInterfaces;
using MediatR;

namespace Application.Configuration.Query.GetSystemConfiguration;

public class GetSystemConfigurationQueryHandler : IRequestHandler<GetSystemConfigurationQuery, GetSystemConfigurationQueryResponse>
{
    private readonly ISystemConfigurationRepository _systemConfigurationRepository;
    public GetSystemConfigurationQueryHandler(ISystemConfigurationRepository systemConfigurationRepository)
    {
        _systemConfigurationRepository = systemConfigurationRepository;
    }
    public async Task<GetSystemConfigurationQueryResponse> Handle(GetSystemConfigurationQuery request, CancellationToken cancellationToken)
    {
        var config = await _systemConfigurationRepository.GetAsync(cancellationToken);
        return new GetSystemConfigurationQueryResponse(config.DailyCapacity ?? 0,config.MinimumDaysToOrder ?? 0);
    }
}
