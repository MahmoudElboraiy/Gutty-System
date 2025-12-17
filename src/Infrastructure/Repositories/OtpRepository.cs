

using Application.Interfaces;
using Domain.Models.Identity;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace Infrastructure.Repositories;

public class OtpRepository : IOtpRepository
{
    private readonly ApplicationDbContext _context;

    public OtpRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task SaveOtpAsync(string phoneNumber, string code)
    {
        var existing = await _context.UserOtps
                .FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber);

        if (existing != null)
            _context.UserOtps.Remove(existing);

        var otp = new UserOtp
        {
            PhoneNumber = phoneNumber,
            Code = code,
            ExpirationTime = DateTime.UtcNow.AddMinutes(10)
        };

        await _context.UserOtps.AddAsync(otp);
        await _context.SaveChangesAsync();
    }
    public async Task SaveOtpAsync(string phoneNumber, string code,bool isVerfied)
    {
        var existing = await _context.UserOtps
                .FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber);

        if (existing != null)
            _context.UserOtps.Remove(existing);

        var otp = new UserOtp
        {
            PhoneNumber = phoneNumber,
            Code = code,
            IsVerified = isVerfied,
            ExpirationTime = DateTime.UtcNow.AddMinutes(10)
        };

        await _context.UserOtps.AddAsync(otp);
        await _context.SaveChangesAsync();
    }
    public async Task<UserOtp?> GetOtpAsync(string phoneNumber)
    {
        return await _context.UserOtps
            .FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber);
    }
    public async Task VerifyOtpAsync(string phoneNumber)
    {
        var otp = await GetOtpAsync(phoneNumber);
        if (otp != null)
        {
            otp.IsVerified = true;
            await _context.SaveChangesAsync();
        }
    }

    public async Task RemoveOtpAsync(string phoneNumber)
    {
        var otp = await GetOtpAsync(phoneNumber);
        if (otp != null)
        {
            _context.UserOtps.Remove(otp);
            await _context.SaveChangesAsync();
        }
    }
}
