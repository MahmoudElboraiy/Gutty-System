

using ErrorOr;
using MediatR;

namespace Application.Authentication.Common.EditUserDashBoard;

public record EditUserDashBoardCommand(
    string UserId,
    string? Name,
    string? PhoneNumber,
    string? Address,
    string? SecondaryAddress
) : IRequest<ErrorOr<ResultSuccess>>;
public record ResultSuccess(
    bool IsSuccess,
    string Message
);
