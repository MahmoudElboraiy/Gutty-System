using Domain.DErrors;
using Domain.Models.Identity;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Authentication.Queries.UserVerify;

public class UserVerifyQueryHandler : IRequestHandler<UserVerifyQuery, ErrorOr<UserVerifyQueryResponse>>
{
    private readonly UserManager<User> _userManager;

    public UserVerifyQueryHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<ErrorOr<UserVerifyQueryResponse>> Handle(UserVerifyQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);
        
        if (user == null)
        {
            return DomainErrors.Authentication.UserNotFound(request.UserId);
        }
        
        var roles = await _userManager.GetRolesAsync(user);
        
        return new UserVerifyQueryResponse(
                user.Id,
                user.PhoneNumber,
                user.FirstName,
                user.MiddleName,
                user.LastName,
                user.MainAddress,
                user.SecondPhoneNumber,
                user.Email,
                user.PhoneNumberConfirmed,
                roles
            );
    }
}