using System.Data;
using Application.Profiles;
using Domain.Models.Identity;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Queries.GetUsers;

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, ErrorOr<GetUsersQueryResponse>>
{
    private readonly UserManager<User> _userManager;

    public GetUsersQueryHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<ErrorOr<GetUsersQueryResponse>> Handle(
        GetUsersQuery request,
        CancellationToken cancellationToken
    )
    {
        var users = _userManager.Users;

        var count = users.Count();
        var totalPages = (int)Math.Ceiling(count / (double)request.PageSize);
        var currentPage = request.PageNumber > totalPages ? totalPages : request.PageNumber;
        var hasNextPage = currentPage < totalPages;
        var hasPreviousPage = currentPage > 1;

        var usersToReturn = users
            .Where(user =>
                user.PhoneNumber.Contains(request.SearchTerm)
                || user.Id.Contains(request.SearchTerm)
            )
            .OrderBy(user => user.Id)
            .Skip((currentPage - 1) * request.PageSize)
            .Take(request.PageSize);

        var responseUsers = await usersToReturn
            .Select(user => user.MapUserResponse())
            .ToListAsync(cancellationToken: cancellationToken);

        var response = new GetUsersQueryResponse(
            responseUsers,
            currentPage,
            request.PageSize,
            count,
            totalPages,
            hasPreviousPage,
            hasNextPage
        );

        return response;
    }
}
