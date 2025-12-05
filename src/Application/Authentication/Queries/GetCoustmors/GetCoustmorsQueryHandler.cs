

using Application.Interfaces.UnitOfWorkInterfaces;
using Domain.Enums;
using Domain.Models.Identity;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Authentication.Queries.GetCoustmors;

public class GetCoustmorsQueryHandler:IRequestHandler<GetCoustmorsQuery, ErrorOr<List<GetCoustmorsQueryResponse>>>
{
    private readonly UserManager<User> _userManager;
    public GetCoustmorsQueryHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<ErrorOr<List<GetCoustmorsQueryResponse>>> Handle(
       GetCoustmorsQuery request,
       CancellationToken cancellationToken)
    {
        int skip = (request.pageNumber - 1) * request.pageSize;

        var users = await _userManager.Users
            .Skip(skip)
            .Take(request.pageSize)
            .ToListAsync(cancellationToken);


        var response = users.Select(u => new GetCoustmorsQueryResponse(
            u.Id,
            u.Name,           
            u.PhoneNumber,
            u.MainAddress
        )).ToList();

        return response;
    }

}
