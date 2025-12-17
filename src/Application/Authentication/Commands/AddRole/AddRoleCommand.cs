

using Application.Authentication.Common;
using ErrorOr;
using MediatR;

namespace Application.Authentication.Commands.AddRole;

public record AddRoleCommand(
    string Name,
    string PhoneNumber,
    string Password,
    string MainAddress,
    string? SecondaryAddress,
    string Role
    ): IRequest<ErrorOr<AuthenticationResponse>>;
