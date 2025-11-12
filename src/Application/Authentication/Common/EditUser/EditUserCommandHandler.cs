
using Application.Interfaces;
using Domain.Models.Identity;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Authentication.Common.EditUser;

public class EditUserCommandHandler: IRequestHandler<EditUserCommand, ErrorOr<ResultMessage>>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly UserManager<User> _userManager;
    public EditUserCommandHandler(
        ICurrentUserService currentUserService,
        UserManager<User> userManager
    )
    {
        _currentUserService = currentUserService;
        _userManager = userManager;
    }
    public async Task<ErrorOr<ResultMessage>> Handle(EditUserCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return Error.Failure("User.NotFound", "User not found.");
        }
        user.Name = string.IsNullOrEmpty(request.Name) ? user.Name : request.Name;
        user.MainAddress = string.IsNullOrEmpty(request.MainAddress) ? user.MainAddress : request.MainAddress;
        user.SecondaryAddress = string.IsNullOrEmpty(request.SecondaryAddress) ? user.SecondaryAddress : request.SecondaryAddress;

        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            return Error.Failure("User.UpdateFailed", "Failed to update user.");
        }
        return new ResultMessage(true, "User updated successfully.");
    }
}
