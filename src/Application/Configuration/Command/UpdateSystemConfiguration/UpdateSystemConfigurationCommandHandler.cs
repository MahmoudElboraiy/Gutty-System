
using Application.Interfaces.UnitOfWorkInterfaces;
using Domain.Models.Entities;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Configuration.Command.UpdateSystemConfiguration;

public record UpdateSystemConfigurationCommandHandler : IRequestHandler<UpdateSystemConfigurationCommand, UpdateSystemConfigurationCommandResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    public UpdateSystemConfigurationCommandHandler(ISystemConfigurationRepository systemConfigurationRepository, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<UpdateSystemConfigurationCommandResponse> Handle(UpdateSystemConfigurationCommand request, CancellationToken cancellationToken)
    {
        var config = await _unitOfWork
          .Configurations
          .GetQueryable()
          .AsNoTracking()
          .FirstOrDefaultAsync(cancellationToken);
        if (config == null)
        {
            var newConfig = new Configurations
            {
                DailyCapacity = request.DailyCapacity,
                MinimumDaysToOrder = request.MinimumDaysToOrder,
                MaximumDaysToOrder = request.MaximumDaysToOrder
            };
            await _unitOfWork.Configurations.AddAsync(newConfig);
        }
        else
        {
            config.DailyCapacity = request.DailyCapacity;
            config.MinimumDaysToOrder = request.MinimumDaysToOrder;
            config.MaximumDaysToOrder = request.MaximumDaysToOrder;
            _unitOfWork.Configurations.Update(config);
        }
        await _unitOfWork.CompleteAsync();
        return new UpdateSystemConfigurationCommandResponse(true,"Updated Successfuly.");
    }
}
