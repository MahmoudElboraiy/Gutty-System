

using ErrorOr;
using MediatR;

namespace Application.Authentication.Commands.UserRegisterTemp;

public record UserRegisterTempCommand(
    string FirstName,
    string MiddleName,
    string LastName,
    string PhoneNumber,
    string Password,
    string MainAddress,
    string SecondaryAddress,
    int CityId
) : IRequest<ErrorOr<string>>;
