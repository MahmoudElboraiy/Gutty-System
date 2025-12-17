

using Application.Interfaces.UnitOfWorkInterfaces;
using Domain.Enums;
using Domain.Models.Identity;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Authentication.Queries.GetCoustmors;

public class GetCoustmorsQueryHandler:IRequestHandler<GetCoustmorsQuery, ErrorOr<GetCoustmorsQueryResponse>>
{
    private readonly UserManager<User> _userManager;
    private readonly IUnitOfWork _unitOfWork;
    public GetCoustmorsQueryHandler(UserManager<User> userManager, IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<GetCoustmorsQueryResponse>> Handle(
       GetCoustmorsQuery request,
       CancellationToken cancellationToken)
    {
        var query = _userManager.Users
        .AsNoTracking();
        if (!string.IsNullOrWhiteSpace(request.searchName))
        {
            string search = request.searchName.ToLower();

            query = query.Where(u =>
                u.Name.ToLower().Contains(search)
            );
        }
        int totalCount = await query.CountAsync(cancellationToken);
        int skip = (request.pageNumber - 1) * request.pageSize;
        var users = await query
         .OrderBy(u => u.Name)
         .Skip(skip)
         .Take(request.pageSize)
         .ToListAsync(cancellationToken);

        var userIds = users.Select(u => u.Id).ToList();

        var subscriptions = await _unitOfWork.Subscriptions
            .GetQueryable()
            .AsNoTracking()
            .Include(s => s.Plan)
            .Where(s => userIds.Contains(s.UserId) && s.IsCurrent)
            .ToListAsync(cancellationToken);

        // 6) Last orders
        var lastOrders = await _unitOfWork.Orders
            .GetQueryable()
            .AsNoTracking()
            .Where(o => subscriptions.Select(s => s.Id).Contains(o.SubscriptionId))
            .GroupBy(o => o.Subscription.UserId)
            .Select(g => new
            {
                UserId = g.Key,
                LastOrder = g.Max(o => o.OrderDate)
            })
            .ToListAsync(cancellationToken);

        var customers = users.Select(u =>
        {
            var subscription = subscriptions.FirstOrDefault(s => s.UserId == u.Id);
            var lastOrder = lastOrders.FirstOrDefault(l => l.UserId == u.Id);

            return new GetCoustmorsItem(
                Id: u.Id,
                Name: u.Name,
                PhoneNumber: u.PhoneNumber,
                MainAddress: u.MainAddress,
                SubcsriptionPlan: subscription?.Plan?.Name,
                LastOrder: lastOrder?.LastOrder
            );
        }).ToList();

        return new GetCoustmorsQueryResponse(
            pageNumber: request.pageNumber,
            pageSize: request.pageSize,
            TotalCount: totalCount,
            Customers: customers
        );
    }

}
