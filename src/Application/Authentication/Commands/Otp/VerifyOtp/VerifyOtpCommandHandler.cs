using Application.Authentication.Commands.ForgetPassword.VerifyForgetPasswordOtp;
using Application.Interfaces;
using Domain.Models.Identity;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authentication.Commands.Otp.VerifyOtp
{
    public class VerifyOtpCommandHandler
     : IRequestHandler<VerifyOtpCommand, ErrorOr<string>>
    {
        private readonly IOtpRepository _otpRepository;
        private readonly UserManager<User> _userManager;

        public VerifyOtpCommandHandler(IOtpRepository otpRepository)
        {
            _otpRepository = otpRepository;
        }

        public async Task<ErrorOr<string>> Handle(VerifyOtpCommand request, CancellationToken cancellationToken)
        {
            var cachedOtp = await _otpRepository.GetOtpAsync(request.PhoneNumber);

            if (cachedOtp == null || cachedOtp.Code != request.OtpCode )
                return Error.Validation("Otp.Invalid", "The verification code is incorrect");
            if (cachedOtp.ExpirationTime < DateTime.UtcNow)
            {
                await _otpRepository.RemoveOtpAsync(request.PhoneNumber);
                return Error.Validation("Otp.Expired", "The verification code has expired.");
            }

            //   await _otpRepository.SaveOtpAsync($"{request.PhoneNumber}", cachedOtp.Code, true);

            await _otpRepository.RemoveOtpAsync(request.PhoneNumber);

            return "The code has been successfully verified.";
        }
    }
}
