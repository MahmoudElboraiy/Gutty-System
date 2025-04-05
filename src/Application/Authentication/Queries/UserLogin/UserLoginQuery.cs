using Application.Authentication.Common;
using ErrorOr;
using MediatR;

namespace Application.Authentication.Queries.UserLogin;

public record UserLoginQuery(string PhoneNumber, string Password)
    : IRequest<ErrorOr<AuthenticationResponse>>;
