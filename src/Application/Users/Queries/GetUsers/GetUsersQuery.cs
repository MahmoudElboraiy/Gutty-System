using ErrorOr;
using MediatR;

namespace Application.Users.Queries.GetUsers;

public record GetUsersQuery(string? SearchTerm = "", int PageNumber = 1, int PageSize = 20)
    : IRequest<ErrorOr<GetUsersQueryResponse>>;
