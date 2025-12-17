
using ErrorOr;
using MediatR;

namespace Application.Authentication.Commands.VerifyRegisterOtp;

public record VerifyRegisterOtpCommand(string PhoneNumber, string Code) : IRequest<ErrorOr<string>>;
