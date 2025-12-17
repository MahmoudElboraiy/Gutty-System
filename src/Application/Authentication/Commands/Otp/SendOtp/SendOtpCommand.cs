using ErrorOr;
using MediatR;

namespace Application.Authentication.Commands.Otp.SendOtp;

public record SendOtpCommand(string PhoneNumber) : IRequest<ErrorOr<string>>;