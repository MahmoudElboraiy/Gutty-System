

using Application.Authentication.Common;
using ErrorOr;
using MediatR;

namespace Application.Authentication.Commands.AddRole;

public record AddRoleCommand(
    string FirstName,
    string MiddleName,
    string LastName,
    string PhoneNumber,
    string Password,
    string MainAddress,
    string? SecondaryAddress,
    string Role
    ): IRequest<ErrorOr<AuthenticationResponse>>;
