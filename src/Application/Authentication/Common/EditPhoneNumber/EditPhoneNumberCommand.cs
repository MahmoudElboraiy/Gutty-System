
using ErrorOr;
using MediatR;

namespace Application.Authentication.Common.EditPhoneNumber;

public record EditPhoneNumberCommand(string NewPhoneNumber) : IRequest<ErrorOr<ResultMessage>>;
public record ResultMessage(
    bool IsSuccess,
    string Message
);
