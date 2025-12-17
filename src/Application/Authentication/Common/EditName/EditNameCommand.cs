
using ErrorOr;
using MediatR;

namespace Application.Authentication.Common.EditName;

public record EditNameCommand(string Name):IRequest<ErrorOr<ResultMessage>>;
public record ResultMessage (
    bool IsSuccess,
    string Message
);