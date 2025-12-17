

using ErrorOr;
using MediatR;

namespace Application.Authentication.Common.EditUser;

public record EditUserCommand(
    string Name,
    string PhoneNumber,
    string MainAddress,
    string? SecondaryAddress
    ):IRequest<ErrorOr<ResultMessage>>;

public record ResultMessage(
    bool IsSuccess,
    string Message
);
