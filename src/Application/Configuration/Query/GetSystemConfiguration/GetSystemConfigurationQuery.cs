
using MediatR;

namespace Application.Configuration.Query.GetSystemConfiguration;

public record GetSystemConfigurationQuery : IRequest<GetSystemConfigurationQueryResponse>;
public record GetSystemConfigurationQueryResponse(int DailyCapacity, int MinimumDaysToOrder);
