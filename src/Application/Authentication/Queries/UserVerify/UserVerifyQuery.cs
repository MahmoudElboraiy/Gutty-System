using MediatR;
using ErrorOr;

namespace Application.Authentication.Queries.UserVerify;

public record UserVerifyQuery(
    string UserId
    ) : IRequest<ErrorOr<UserVerifyQueryResponse>>;