

using Domain.Enums;
using ErrorOr;
using MediatR;

namespace Application.PromoCodes.Commands.CreatePromoCode;

public record CreatePromoCodeCommand(
    string Code,
    DiscountType DiscountType,
    decimal DiscountValue,
    DateTime ExpiryDate,
    bool IsActive
) : IRequest<ErrorOr<CreatePromoCodeCommandResponse>>;
public record CreatePromoCodeCommandResponse(
    Guid Id
);

