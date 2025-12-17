

using ErrorOr;
using MediatR;

namespace Application.Authentication.Commands.ForgetPassword.VerifyForgetPasswordOtp;

public record VerifyForgetPasswordOtpCommand(string OtpCode, string PhoneNumber) : IRequest<ErrorOr<string>>;
