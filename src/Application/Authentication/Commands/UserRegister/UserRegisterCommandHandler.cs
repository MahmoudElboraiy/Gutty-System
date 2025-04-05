using Application.Authentication.Common;
using Application.Interfaces;
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
    private readonly IJwtTokenGenerator JwtTokenGenerator;

    public UserRegisterCommandHandler(
        UserManager<User> userManager,
        IJwtTokenGenerator jwtTokenGenerator
    )
    {
        _userManager = userManager;
        JwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<ErrorOr<AuthenticationResponse>> Handle(
        UserRegisterCommand request,
        CancellationToken cancellationToken
    )
    {
        var user = new User
        {
            FirstName = request.FirstName,
            MiddleName = request.MiddleName,
            LastName = request.LastName,
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

        // TODO: Send verification sms to user`

        var jwtToken = JwtTokenGenerator.GenerateToken(user, Roles.User.ToString());

        return new AuthenticationResponse(jwtToken, Roles.User.ToString());
    }
}
