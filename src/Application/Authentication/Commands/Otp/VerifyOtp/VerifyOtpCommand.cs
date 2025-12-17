using ErrorOr;
using MediatR;

namespace Application.Authentication.Commands.Otp.VerifyOtp;

public record VerifyOtpCommand(string PhoneNumber, string OtpCode) : IRequest<ErrorOr<string>>;
