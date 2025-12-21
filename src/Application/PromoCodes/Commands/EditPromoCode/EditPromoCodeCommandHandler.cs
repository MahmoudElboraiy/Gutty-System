

using Application.Cache;
using Application.Interfaces;
using Application.Interfaces.UnitOfWorkInterfaces;
using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.PromoCodes.Commands.EditPromoCode;

public class EditPromoCodeCommandHandler : IRequestHandler<EditPromoCodeCommand, ErrorOr<EditPromoCodeCommandResponse>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly ICacheService _cacheService;
    public EditPromoCodeCommandHandler(IUnitOfWork unitOfWork, ICacheService cacheService)
    {
        this.unitOfWork = unitOfWork;
        _cacheService = cacheService;
    }
    public async Task<ErrorOr<EditPromoCodeCommandResponse>> Handle(EditPromoCodeCommand request, CancellationToken cancellationToken)
    {
        var promoCode = await unitOfWork.PromoCodes.GetByIdAsync(request.Id);
        if (promoCode == null)
        {
            return Error.NotFound("PromoCode.NotFound", "Promo code not found.");
        }
        var existingPromoCode = await unitOfWork.PromoCodes
            .GetQueryable()
            .FirstOrDefaultAsync(pc => pc.Code == request.Code && pc.Id != request.Id, cancellationToken);
        if (existingPromoCode != null)
        {
            return Error.Conflict("PromoCode.DuplicateCode", "A promo code with the same code already exists.");
        }
        promoCode.Code = request.Code;
        promoCode.DiscountType = request.DiscountType;
        promoCode.DiscountValue = request.DiscountValue;
        promoCode.ExpiryDate = request.ExpiryDate;
        promoCode.IsActive = request.IsActive;
        _cacheService.IncrementVersion(CacheKeys.PromoCodesVersion);
        unitOfWork.PromoCodes.Update(promoCode);
        await unitOfWork.CompleteAsync();
        return new EditPromoCodeCommandResponse(promoCode.Id);
    }
}
