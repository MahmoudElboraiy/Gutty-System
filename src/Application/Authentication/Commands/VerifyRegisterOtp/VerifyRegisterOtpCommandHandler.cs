

using Application.Interfaces;
using Domain.Enums;
using Domain.Models.Identity;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Authentication.Commands.VerifyRegisterOtp;

public class VerifyRegisterOtpCommandHandler : IRequestHandler<VerifyRegisterOtpCommand, ErrorOr<string>>
{
    private readonly IOtpRepository _otpRepository;
    private readonly UserManager<User> _userManager;
    private readonly IJwtTokenGenerator _jwt;

    public VerifyRegisterOtpCommandHandler(IOtpRepository otpRepository, UserManager<User> userManager, IJwtTokenGenerator jwt)
    {
        _otpRepository = otpRepository;
        _userManager = userManager;
        _jwt = jwt;
    }

    public async Task<ErrorOr<string>> Handle(VerifyRegisterOtpCommand request, CancellationToken cancellationToken)
    {
        var otp = await _otpRepository.GetOtpAsync(request.PhoneNumber);
        if (otp == null || otp != request.Code)
            return ErrorOr.Error.Validation("Otp.Invalid", "The verification code is incorrect or expired.");

        // استرجع بيانات المستخدم المؤقتة
        var userJson = await _otpRepository.GetOtpAsync($"{request.PhoneNumber}:user");
        if (userJson == null)
            return ErrorOr.Error.NotFound("User.Pending", "Temporary user data does not exist.");

        var pendingUser = System.Text.Json.JsonSerializer.Deserialize<TempUserData>(userJson)!;

        // إنشاء المستخدم في AspNetUsers
        var user = new User
        {
            FirstName = pendingUser.FirstName,
            MiddleName = pendingUser.MiddleName,
            LastName = pendingUser.LastName,
            PhoneNumber = pendingUser.PhoneNumber,
            UserName = pendingUser.PhoneNumber,
            MainAddress = pendingUser.MainAddress,
            SecondaryAddress = pendingUser.SecondaryAddress,
            CityId = pendingUser.CityId
        };

        var result = await _userManager.CreateAsync(user, pendingUser.Password);
        if (!result.Succeeded)
            return ErrorOr.Error.Failure("Register.Failed", "An error occurred while creating the account.");

        await _userManager.AddToRoleAsync(user, Roles.User.ToString());

        // نظّف الكاش
        await _otpRepository.RemoveOtpAsync(request.PhoneNumber);
        await _otpRepository.RemoveOtpAsync($"{request.PhoneNumber}:user");

        var token = _jwt.GenerateToken(user, Roles.User.ToString());
        return $"Account activated successfully.: {token}";
    }
    private class TempUserData
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string MainAddress { get; set; }
        public string SecondaryAddress { get; set; }
        public int CityId { get; set; }
    }
}