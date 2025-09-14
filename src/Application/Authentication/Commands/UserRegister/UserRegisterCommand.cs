using Application.Authentication.Common;
using ErrorOr;
using MediatR;

namespace Application.Authentication.Commands.UserRegister;

public record UserRegisterCommand(
    string FirstName,
    string MiddleName,
    string LastName,
    string PhoneNumber,
    string Password,
    string MainAddress,
    string? SecondaryAddress,
    int CityId
) : IRequest<ErrorOr<AuthenticationResponse>>;
