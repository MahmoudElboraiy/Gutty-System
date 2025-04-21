using Application.Items.Queries.GetItem;
using ErrorOr;
using MediatR;

namespace Application.Items.Queries.GetItems;

public record GetItemsQuery(string? SearchText) : IRequest<ErrorOr<GetItemsQueryResponse>>;

public record GetItemsQueryResponse(List<GetItemQueryResponse> Items);
