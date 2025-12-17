
using Application.Interfaces.UnitOfWorkInterfaces;
using Domain.Models.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class SystemConfigurationRepository : ISystemConfigurationRepository
{
    private readonly ApplicationDbContext _context;
    public SystemConfigurationRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<SystemConfiguration> GetAsync(CancellationToken cancellationToken)
    {
        var config = await _context.SystemConfigurations.FirstOrDefaultAsync(cancellationToken);
        if (config == null)
        {
            config = new SystemConfiguration();
            _context.SystemConfigurations.Add(config);
            await _context.SaveChangesAsync(cancellationToken);
        }
        return config;
    }
    public async Task UpdateAsync(SystemConfiguration config, CancellationToken cancellationToken)
    {
        _context.SystemConfigurations.Update(config);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
