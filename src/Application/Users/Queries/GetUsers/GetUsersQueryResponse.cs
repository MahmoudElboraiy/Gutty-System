using Domain.Models.Identity;

namespace Application.Users.Queries.GetUsers;

public record GetUsersQueryResponse(
    List<UserResponse> Users,
    int PageNumber,
    int PageSize,
    int TotalCount,
    int TotalPages,
    bool HasPreviousPage,
    bool HasNextPage
);

public record UserResponse(
    string Id,
    string PhoneNumber,
    string Name,
    string MainAddress,
    string SecondPhoneNumber,
    string? Email,
    bool PhoneNumberConfirmed
);
