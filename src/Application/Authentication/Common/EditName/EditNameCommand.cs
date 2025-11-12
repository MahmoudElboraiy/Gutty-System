
using ErrorOr;
using MediatR;

namespace Application.Authentication.Common.EditName;

public record EditNameCommand(string? FirstName,string? MiddleName, string? LastName):IRequest<ErrorOr<ResultMessage>>;
public record ResultMessage (
    bool IsSuccess,
    string Message
);