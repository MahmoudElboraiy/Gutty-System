

using Domain.Models.Identity;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Authentication.Common.EditUserDashBoard;

public class EditUserDashBoardCommandHandler : IRequestHandler<EditUserDashBoardCommand, ErrorOr<ResultSuccess>>
{
    private readonly UserManager<User> _userManager;
    public EditUserDashBoardCommandHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }
    public async Task<ErrorOr<ResultSuccess>> Handle(EditUserDashBoardCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);
        if (user == null)
        {
            return Error.Failure("User.NotFound", "User not found.");
        }
        var phoneNumberExists = await _userManager.Users.AnyAsync(
        u => u.PhoneNumber == request.PhoneNumber && u.Id != user.Id,
        cancellationToken: cancellationToken
         );
        if (phoneNumberExists)
        {
            return Error.Failure("User.DuplicatePhoneNumber", "Phone number is already in use.");
        }
        user.PhoneNumber = string.IsNullOrEmpty(request.PhoneNumber) ? user.PhoneNumber : request.PhoneNumber;
        user.Name = string.IsNullOrEmpty(request.Name) ? user.Name : request.Name;
        user.MainAddress = string.IsNullOrEmpty(request.Address) ? user.MainAddress : request.Address;
        user.SecondaryAddress = string.IsNullOrEmpty(request.SecondaryAddress) ? user.SecondaryAddress : request.SecondaryAddress;
        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            return Error.Failure("User.UpdateFailed", "Failed to update user.");
        }
        return new ResultSuccess(true, "User updated successfully.");
    }

}
