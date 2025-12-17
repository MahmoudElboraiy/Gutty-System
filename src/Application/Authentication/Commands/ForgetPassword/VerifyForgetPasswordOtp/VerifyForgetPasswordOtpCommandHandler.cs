
using Application.Interfaces;
using Domain.Models.Identity;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Authentication.Commands.ForgetPassword.VerifyForgetPasswordOtp;

public class VerifyForgetPasswordOtpCommandHandler
    : IRequestHandler<VerifyForgetPasswordOtpCommand, ErrorOr<string>>
{
    private readonly IOtpRepository _otpRepository;
    private readonly UserManager<User> _userManager;

    public VerifyForgetPasswordOtpCommandHandler(IOtpRepository otpRepository )
    {
        _otpRepository = otpRepository;
    }

    public async Task<ErrorOr<string>> Handle(VerifyForgetPasswordOtpCommand request, CancellationToken cancellationToken)
    {
        var cachedOtp = await _otpRepository.GetOtpAsync(request.PhoneNumber);

        if (cachedOtp == null || cachedOtp.Code != request.OtpCode)
            return Error.Validation("Otp.Invalid", "The verification code is incorrect or expired.");

        // نحفظ حالة التحقق المؤقتة لمدة قصيرة (مثلاً 10 دقايق)
        await _otpRepository.SaveOtpAsync($"{request.PhoneNumber}", cachedOtp.Code,true);

        // نحذف الـ OTP القديم علشان الأمان
       // await _otpRepository.RemoveOtpAsync(request.PhoneNumber);

        return "The code has been successfully verified.";
    }
}
