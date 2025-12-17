

using Domain.Enums;
using ErrorOr;
using MediatR;

namespace Application.PromoCodes.Commands.EditPromoCode;

public record EditPromoCodeCommand(
    Guid Id,
    string Code,
    DiscountType DiscountType,
    decimal DiscountValue,
    DateTime ExpiryDate,
    bool IsActive
) : IRequest<ErrorOr<EditPromoCodeCommandResponse>>;
public record EditPromoCodeCommandResponse(
    Guid Id
);
