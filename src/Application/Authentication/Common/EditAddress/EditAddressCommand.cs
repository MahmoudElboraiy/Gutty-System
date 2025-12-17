

using ErrorOr;
using MediatR;

namespace Application.Authentication.Common.EditAddress;

public record EditAddressCommand(string NewMainAddress, string? NewSecondaryAddress) : IRequest<ErrorOr<ResultMessage>>;
public record ResultMessage(
    bool IsSuccess,
    string Message
);
