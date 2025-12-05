

using Application.Interfaces.UnitOfWorkInterfaces;
using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.PromoCodes.Commands.CreatePromoCode;

public class CreatePromoCodeCommandHandler : IRequestHandler<CreatePromoCodeCommand, ErrorOr<CreatePromoCodeCommandResponse>>
{
    private readonly IUnitOfWork unitOfWork;
    public CreatePromoCodeCommandHandler(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }
    public async Task<ErrorOr<CreatePromoCodeCommandResponse>> Handle(CreatePromoCodeCommand request, CancellationToken cancellationToken)
    {
        var existingPromoCode = await unitOfWork.PromoCodes
            .GetQueryable()
            .FirstOrDefaultAsync(pc => pc.Code == request.Code, cancellationToken);

        if(existingPromoCode != null)
        {
            return Error.Conflict("PromoCode.DuplicateCode", "A promo code with the same code already exists.");
        }
        var promoCode = new Domain.Models.Entities.PromoCode
        {
            Code = request.Code,
            DiscountType = request.DiscountType,
            DiscountValue = request.DiscountValue,
            ExpiryDate = request.ExpiryDate,
            IsActive = request.IsActive
        };
        await unitOfWork.PromoCodes.AddAsync(promoCode);
        await unitOfWork.CompleteAsync();
        return new CreatePromoCodeCommandResponse(promoCode.Id);
    }

}
