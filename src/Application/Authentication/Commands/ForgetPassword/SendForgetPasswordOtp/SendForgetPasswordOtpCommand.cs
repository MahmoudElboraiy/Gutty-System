

using ErrorOr;
using MediatR;

namespace Application.Authentication.Commands.ForgetPassword.SendForgetPasswordOtp;


public record SendForgetPasswordOtpCommand(string PhoneNumber) : IRequest<ErrorOr<string>>;

