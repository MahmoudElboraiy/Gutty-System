

using ErrorOr;
using MediatR;

namespace Application.Authentication.Commands.ForgetPassword.ResetPassword;

public record ResetPasswordCommand(string PhoneNumber, string NewPassword) : IRequest<ErrorOr<string>>;

