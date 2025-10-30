using Application.Interfaces;
using Domain.Models.Identity;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace Application.Authentication.Commands.ForgetPassword.SendForgetPasswordOtp;

public class SendForgetPasswordOtpCommandHandler
: IRequestHandler<SendForgetPasswordOtpCommand, ErrorOr<string>>
{
    private readonly UserManager<User> _userManager;
    private readonly ISmsRepository _smsRepository;
    private readonly IOtpRepository _otpRepository;

    public SendForgetPasswordOtpCommandHandler(
        UserManager<User> userManager,
        ISmsRepository smsRepository,
        IOtpRepository otpRepository)
    {
        _userManager = userManager;
        _smsRepository = smsRepository;
        _otpRepository = otpRepository;
    }

    public async Task<ErrorOr<string>> Handle(SendForgetPasswordOtpCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == request.PhoneNumber, cancellationToken);
        if (user is null)
            return Error.NotFound("ForgetPassword.UserNotFound", "User with this phone number was not found.");

        var otp = new Random().Next(100000, 999999).ToString();

        await _otpRepository.SaveOtpAsync(request.PhoneNumber, otp);
        await _smsRepository.SendSmsAsync(request.PhoneNumber, $"The reset code is : {otp}");

        return "Password recovery code successfully sent.";
    }
}
