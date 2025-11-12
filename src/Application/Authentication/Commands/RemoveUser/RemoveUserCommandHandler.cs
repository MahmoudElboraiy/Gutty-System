
using Domain.Models.Identity;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Authentication.Commands.RemoveUser;

public class RemoveUserCommandHandler : IRequestHandler<RemoveUserCommand, ErrorOr<ResultSuccess>>
{
    private readonly UserManager<User> _userManager;
    public RemoveUserCommandHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }
    public async Task<ErrorOr<ResultSuccess>> Handle(RemoveUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.PhoneNumber);

        if (user == null)
        {
            return Error.Failure("User.NotFound", "User not found.");
        }

        var result = await _userManager.DeleteAsync(user);

        if (!result.Succeeded)
        {
            return Error.Failure("User.DeletionFailed", "Failed to delete user.");
        }

        return new ResultSuccess(true, "User deleted successfully.");
    }
}
