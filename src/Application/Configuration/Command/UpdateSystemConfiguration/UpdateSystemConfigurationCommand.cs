
using MediatR;

namespace Application.Configuration.Command.UpdateSystemConfiguration;

public record UpdateSystemConfigurationCommand(int? DailyCapacity, int? MinimumDaysToOrder) : IRequest<UpdateSystemConfigurationCommandResponse>;
public record UpdateSystemConfigurationCommandResponse(bool Success, string Message);
