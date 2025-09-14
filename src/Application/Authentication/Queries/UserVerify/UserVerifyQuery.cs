using ErrorOr;
using MediatR;

namespace Application.Authentication.Queries.UserVerify;

public record UserVerifyQuery(string UserId) : IRequest<ErrorOr<UserVerifyQueryResponse>>;
