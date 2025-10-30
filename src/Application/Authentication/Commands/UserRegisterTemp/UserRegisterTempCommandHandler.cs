

using Application.Interfaces;
using Domain.Models.Identity;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Authentication.Commands.UserRegisterTemp;

public class UserRegisterTempCommandHandler : IRequestHandler<UserRegisterTempCommand, ErrorOr<string>>
{
    private readonly IOtpRepository _otpRepository;
    private readonly ISmsRepository _smsRepository;
    private readonly UserManager<User> _userManager;

    public UserRegisterTempCommandHandler(IOtpRepository otpRepository, ISmsRepository smsRepository, UserManager<User> userManager)
    {
        _otpRepository = otpRepository;
        _smsRepository = smsRepository;
        _userManager = userManager;
    }

    public async Task<ErrorOr<string>> Handle(UserRegisterTempCommand request, CancellationToken cancellationToken)
    {
        var exists = await _userManager.Users.AnyAsync(u => u.PhoneNumber == request.PhoneNumber, cancellationToken);
        if (exists)
            return Error.Conflict("Register.DuplicatePhone", "This phone number already exists");

        
        var otp = new Random().Next(100000, 999999).ToString();
        await _otpRepository.SaveOtpAsync(request.PhoneNumber, otp);
        await _smsRepository.SendSmsAsync(request.PhoneNumber, $"Verification code is : {otp}");

        var pendingUser = new
        {
            request.FirstName,
            request.MiddleName,
            request.LastName,
            request.PhoneNumber,
            request.Password,
            request.MainAddress,
            request.SecondaryAddress,
            request.CityId
        };

        var json = System.Text.Json.JsonSerializer.Serialize(pendingUser);
        await _otpRepository.SaveOtpAsync($"{request.PhoneNumber}:user", json);

        return "Verification code sent successfully, please enter it to activate your account.";
    }
}