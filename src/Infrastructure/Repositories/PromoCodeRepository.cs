using Application.Interfaces.UnitOfWorkInterfaces;
using Domain.Models.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class PromoCodeRepository : IPromoCodeRepository
{
    private readonly ApplicationDbContext _context;

    public PromoCodeRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task<PromoCode> GetPromoCodeByCode(string code)
    {
        return _context.PromoCodes.FirstOrDefaultAsync(x => x.Code == code);
    }

    public Task AddPromoCode(PromoCode promoCode)
    {
        _context.PromoCodes.Add(promoCode);

        return Task.CompletedTask;
    }

    public Task UpdatePromoCode(PromoCode promoCode)
    {
        _context.PromoCodes.Update(promoCode);

        return Task.CompletedTask;
    }

    public Task DeletePromoCode(PromoCode promoCode)
    {
        _context.PromoCodes.Remove(promoCode);

        return Task.CompletedTask;
    }

    public Task<List<PromoCode>> GetAllPromoCodes()
    {
        return _context.PromoCodes.ToListAsync();
    }

    public Task<PromoCode> GetPromoCodeById(int id)
    {
        return _context.PromoCodes.FirstOrDefaultAsync(x => x.Id == id);
    }

    public Task<bool> PromoCodeExists(string code)
    {
        return _context.PromoCodes.AnyAsync(x => x.Code == code);
    }
}
