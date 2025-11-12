using Application.Authentication.Common;
using Application.Interfaces;
using Application.Interfaces.UnitOfWorkInterfaces;
using Domain.DErrors;
using Domain.Enums;
using Domain.Models.Identity;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Authentication.Commands.UserRegister;

public class UserRegisterCommandHandler
    : IRequestHandler<UserRegisterCommand, ErrorOr<AuthenticationResponse>>
{
    private readonly UserManager<User> _userManager;
    private readonly ISmsRepository _smsRepository;
    private readonly IJwtTokenGenerator JwtTokenGenerator;
    private readonly IOtpRepository _otpRepository;

    public UserRegisterCommandHandler(
        UserManager<User> userManager,
        IJwtTokenGenerator jwtTokenGenerator,
        ISmsRepository smsRepository,
        IOtpRepository otpRepository
    )
    {
        _userManager = userManager;
        JwtTokenGenerator = jwtTokenGenerator;
        _smsRepository = smsRepository;
        _otpRepository = otpRepository;
    }

    public async Task<ErrorOr<AuthenticationResponse>> Handle(
        UserRegisterCommand request,
        CancellationToken cancellationToken
    )
    {
       // var verified = await _otpRepository.GetOtpAsync($"{request.PhoneNumber}");

       // if (verified == null || !verified.IsVerified)
       //     return Error.Validation("Otp.NotVerified", "The code must be verified first before resetting.");
        
        var user = new User
        {
            Name = request.Name,
            PhoneNumber = request.PhoneNumber,
            MainAddress = request.MainAddress,
            SecondaryAddress = request.SecondaryAddress,
            UserName = request.PhoneNumber,
        };

        var phoneExists = await _userManager.Users.AnyAsync(
            u => u.PhoneNumber == user.PhoneNumber,
            cancellationToken: cancellationToken
        );
        if (phoneExists)
        {
            return DomainErrors.Authentication.DuplicatePhoneNumber(user.PhoneNumber);
        }

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            return DomainErrors.Authentication.InvalidCredentials();
        }

        await _userManager.AddToRoleAsync(user, Roles.User.ToString());

     //   await _otpRepository.RemoveOtpAsync($"{request.PhoneNumber}");

        // TODO: Send verification sms to user`

        var jwtToken = JwtTokenGenerator.GenerateToken(user, Roles.User.ToString());

        return new AuthenticationResponse(jwtToken, Roles.User.ToString());
    }
}
