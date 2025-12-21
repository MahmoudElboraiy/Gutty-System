

using Application.Cache;
using Application.Interfaces;
using Application.Interfaces.UnitOfWorkInterfaces;
using Domain.Models.Entities;
using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.PromoCodes.Commands.DeletePromoCode;

public class DeletePromoCodeCommandHandler : IRequestHandler<DeletePromoCodeCommand, ErrorOr<DeletePromoCodeCommandResponse>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly ICacheService _cacheService;
    public DeletePromoCodeCommandHandler(IUnitOfWork unitOfWork,ICacheService cacheService)
    {
        this.unitOfWork = unitOfWork;
        _cacheService = cacheService;
    }
    public async Task<ErrorOr<DeletePromoCodeCommandResponse>> Handle(DeletePromoCodeCommand request, CancellationToken cancellationToken)
    {
        var promoCode = await unitOfWork.PromoCodes
            .GetQueryable()
            .FirstOrDefaultAsync(pc => pc.Code == request.code, cancellationToken);
        if (promoCode == null)
        {
            return Error.NotFound("PromoCode.NotFound", "Promo code not found.");
        }
        bool isUsed = await unitOfWork.Subscriptions
       .GetQueryable()
        .AsNoTracking()
       .AnyAsync(s => s.PromoCodeId == promoCode.Id && s.IsCurrent, cancellationToken);

        if (isUsed)
        {
            return Error.Validation(
                code: "PromoCode.DeleteError",
                description: $"Cannot delete promo code '{request.code}' because it is associated with active subscriptions."
                );
        }
        _cacheService.IncrementVersion(CacheKeys.PromoCodesVersion);
        unitOfWork.PromoCodes.Remove(promoCode);
        await unitOfWork.CompleteAsync();
        return new DeletePromoCodeCommandResponse(true);
    }
}
