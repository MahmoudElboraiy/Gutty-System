

using ErrorOr;
using MediatR;

namespace Application.PromoCodes.Commands.DeletePromoCode;

public record DeletePromoCodeCommand(
    string code
) : IRequest<ErrorOr<DeletePromoCodeCommandResponse>>;
public record DeletePromoCodeCommandResponse(
    bool IsDeleted
);
