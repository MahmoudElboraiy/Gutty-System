

using ErrorOr;
using MediatR;

namespace Application.Authentication.Queries.GetCoustmors;

public record GetCoustmorsQuery(int pageNumber, int pageSize,string? searchName): IRequest<ErrorOr<GetCoustmorsQueryResponse>>;
public record GetCoustmorsQueryResponse(
    int pageNumber,
    int pageSize,
    int TotalCount,
    List<GetCoustmorsItem> Customers
    );
public record class GetCoustmorsItem(
    string Id,
    string Name,
    string PhoneNumber,
    string MainAddress,
    string? SubcsriptionPlan,
    DateOnly? LastOrder
    );
