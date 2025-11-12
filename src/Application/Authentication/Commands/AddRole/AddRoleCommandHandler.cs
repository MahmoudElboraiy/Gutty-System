
using Application.Authentication.Common;
using Application.Interfaces;
using Domain.Models.Identity;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Authentication.Commands.AddRole;

public class AddRoleCommandHandler :
    IRequestHandler<AddRoleCommand, ErrorOr<AuthenticationResponse>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    public AddRoleCommandHandler(
        IJwtTokenGenerator jwtTokenGenerator,
        UserManager<User> userManager,
        RoleManager<IdentityRole> roleManager
    )
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userManager = userManager;
        _roleManager = roleManager;
    }
    public async Task<ErrorOr<AuthenticationResponse>> Handle(
        AddRoleCommand request,
        CancellationToken cancellationToken
    )
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == request.PhoneNumber);

        if (user == null)
        {
            user = new User
            {
                FirstName = request.FirstName,
                MiddleName = request.MiddleName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                MainAddress = request.MainAddress,
                SecondaryAddress = request.SecondaryAddress,
                UserName = request.PhoneNumber,
            };
            var IsRoleExitsts = await  _roleManager.RoleExistsAsync(request.Role);
            if (!IsRoleExitsts)
            {
                return Error.Failure("User.CreationFailed", "This Role Type Not Exits");
            }
            var createResult = await _userManager.CreateAsync(user, request.Password);
            if (!createResult.Succeeded)
            {
                return Error.Failure("User.CreationFailed", "Failed to create user.");
            }
        }
        var roleResult = await _userManager.AddToRoleAsync(user, request.Role);
        if (!roleResult.Succeeded)
        {
            return Error.Failure("Role.AssignmentFailed", "Failed to assign role to user.");
        }
        var token = _jwtTokenGenerator.GenerateToken(user,request.Role);
        return new AuthenticationResponse(token,request.Role);
    }
}
