
using Domain.Models.Identity;

namespace Application.Interfaces;

public interface IOtpRepository
{
    Task SaveOtpAsync(string phone, string code);
    Task SaveOtpAsync(string phone, string code,bool isVerfied);

    Task<UserOtp?> GetOtpAsync(string phoneNumber);
    Task VerifyOtpAsync(string phoneNumber);
    Task RemoveOtpAsync(string phone);
}
