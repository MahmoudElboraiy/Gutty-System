using Application.Authentication.Common;
using ErrorOr;
using MediatR;

namespace Application.Authentication.Commands.UserRegister;

public record UserRegisterCommand(
    string Name,
    string PhoneNumber,
    string Password,
    string MainAddress,
    string? SecondaryAddress
) : IRequest<ErrorOr<AuthenticationResponse>>;
