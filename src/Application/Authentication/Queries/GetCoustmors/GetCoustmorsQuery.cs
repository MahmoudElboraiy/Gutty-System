

using ErrorOr;
using MediatR;

namespace Application.Authentication.Queries.GetCoustmors;

public record GetCoustmorsQuery(int pageNumber, int pageSize): IRequest<ErrorOr<List<GetCoustmorsQueryResponse>>>;
public record GetCoustmorsQueryResponse(
    string Id,
    string Name,
    string PhoneNumber,
    string MainAddress
    );
