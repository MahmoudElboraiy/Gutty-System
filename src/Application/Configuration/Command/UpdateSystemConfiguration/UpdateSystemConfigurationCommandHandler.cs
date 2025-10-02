
using Application.Interfaces.UnitOfWorkInterfaces;
using MediatR;

namespace Application.Configuration.Command.UpdateSystemConfiguration;

public record UpdateSystemConfigurationCommandHandler : IRequestHandler<UpdateSystemConfigurationCommand, UpdateSystemConfigurationCommandResponse>
{
    private readonly ISystemConfigurationRepository _systemConfigurationRepository;
    private readonly IUnitOfWork _unitOfWork;
    public UpdateSystemConfigurationCommandHandler(ISystemConfigurationRepository systemConfigurationRepository, IUnitOfWork unitOfWork)
    {
        _systemConfigurationRepository = systemConfigurationRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<UpdateSystemConfigurationCommandResponse> Handle(UpdateSystemConfigurationCommand request, CancellationToken cancellationToken)
    {
        var config = await _systemConfigurationRepository.GetAsync(cancellationToken);
        config.DailyCapacity = request.DailyCapacity ?? config.DailyCapacity;
        config.MinimumDaysToOrder = request.MinimumDaysToOrder ?? config.MinimumDaysToOrder;
        await _systemConfigurationRepository.UpdateAsync(config, cancellationToken);
        await _unitOfWork.CompleteAsync();
        return new UpdateSystemConfigurationCommandResponse(true, "Configuration updated successfully.");
    }
}
