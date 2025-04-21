using Domain.Models.Entities;

namespace Application.Interfaces.UnitOfWorkInterfaces;

public interface IPromoCodeRepository
{
    Task<PromoCode> GetPromoCodeByCode(string code);
    Task AddPromoCode(PromoCode promoCode);
    Task UpdatePromoCode(PromoCode promoCode);
    Task DeletePromoCode(PromoCode promoCode);
    Task<List<PromoCode>> GetAllPromoCodes();
    Task<PromoCode> GetPromoCodeById(int id);
    Task<bool> PromoCodeExists(string code);
}
