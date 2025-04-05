using Application.Authentication.Common;
using Application.Interfaces;
using Domain.DErrors;
using Domain.Models.Identity;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Authentication.Queries.UserLogin;

public class UserLoginQueryHandler
    : IRequestHandler<UserLoginQuery, ErrorOr<AuthenticationResponse>>
{
    private readonly UserManager<User> _userManager;
    private readonly IJwtTokenGenerator JwtTokenGenerator;

    public UserLoginQueryHandler(
        UserManager<User> userManager,
        IJwtTokenGenerator jwtTokenGenerator
    )
    {
        _userManager = userManager;
        JwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<ErrorOr<AuthenticationResponse>> Handle(
        UserLoginQuery request,
        CancellationToken cancellationToken
    )
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(
            u => u.PhoneNumber == request.PhoneNumber,
            cancellationToken: cancellationToken
        );
        if (user == null)
        {
            return DomainErrors.Authentication.InvalidCredentials();
        }

        var result = await _userManager.CheckPasswordAsync(user, request.Password);

        if (!result)
        {
            return DomainErrors.Authentication.InvalidCredentials();
        }

        var role = await _userManager.GetRolesAsync(user);

        var jwtToken = JwtTokenGenerator.GenerateToken(user, role.First());

        return new AuthenticationResponse(jwtToken, role.First());
    }
}
