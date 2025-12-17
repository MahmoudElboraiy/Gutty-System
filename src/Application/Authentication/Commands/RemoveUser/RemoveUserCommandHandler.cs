
using Application.Interfaces.UnitOfWorkInterfaces;
using Domain.Models.Identity;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Authentication.Commands.RemoveUser;

public class RemoveUserCommandHandler : IRequestHandler<RemoveUserCommand, ErrorOr<ResultSuccess>>
{
    private readonly UserManager<User> _userManager;
    private readonly IUnitOfWork _unitOfWork;
    public RemoveUserCommandHandler(UserManager<User> userManager, IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _unitOfWork = unitOfWork;
    }
    public async Task<ErrorOr<ResultSuccess>> Handle(RemoveUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == request.userId);
        if (user == null)
        {
            return Error.Failure("User.NotFound", "User not found.");
        }

        var subscriptions = await _unitOfWork.Subscriptions
          .GetQueryable()
          .AsNoTracking()
          .Where(s => s.UserId == user.Id && s.IsCurrent)
          .FirstOrDefaultAsync(cancellationToken);

        if(subscriptions != null)
        {
            return Error.Failure("User.HasActiveSubscription", "Cannot delete user with active subscription.");
        }

            var result = await _userManager.DeleteAsync(user);

        if (!result.Succeeded)
        {
            return Error.Failure("User.DeletionFailed", "Failed to delete user.");
        }

        return new ResultSuccess(true, "User deleted successfully.");
    }
}
