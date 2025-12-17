
using Domain.Models.Entities;

namespace Application.Interfaces.UnitOfWorkInterfaces;

public interface ISystemConfigurationRepository
{
    Task<SystemConfiguration> GetAsync(CancellationToken cancellationToken);
    Task UpdateAsync(SystemConfiguration config, CancellationToken cancellationToken);
}
