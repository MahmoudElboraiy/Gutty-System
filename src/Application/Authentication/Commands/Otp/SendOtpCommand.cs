
using ErrorOr;
using MediatR;

namespace Application.Authentication.Commands.Otp;

public record SendOtpCommand(string PhoneNumber) : IRequest<ErrorOr<string>>;