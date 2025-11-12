

using Application.Interfaces;
using Domain.Models.Identity;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Authentication.Commands.ForgetPassword.ResetPassword;

public class ResetPasswordCommandHandler
 : IRequestHandler<ResetPasswordCommand, ErrorOr<string>>
{
    private readonly IOtpRepository _otpRepository;
    private readonly UserManager<User> _userManager;

    public ResetPasswordCommandHandler(IOtpRepository otpRepository, UserManager<User> userManager)
    {
        _otpRepository = otpRepository;
        _userManager = userManager;
    }

    public async Task<ErrorOr<string>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var verified = await _otpRepository.GetOtpAsync($"{request.PhoneNumber}");

        if (verified==null ||!verified.IsVerified)
            return Error.Validation("Otp.NotVerified", "The code must be verified first before resetting.");

        var user = await _userManager.Users.FirstOrDefaultAsync(
            u => u.PhoneNumber == request.PhoneNumber, cancellationToken);

        if (user == null)
            return Error.NotFound("User.NotFound", "The user was not found.");

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        var result = await _userManager.ResetPasswordAsync(user, token, request.NewPassword);

        if (!result.Succeeded)
            return Error.Failure("Reset.Failed", "Password change failed.");

        await _otpRepository.RemoveOtpAsync($"{request.PhoneNumber}");

        return "The password was successfully updated.";
    }
}
