
namespace Application.Interfaces;

public interface IOtpRepository
{
    Task SaveOtpAsync(string phone, string code);
    Task<string?> GetOtpAsync(string phone);
    Task RemoveOtpAsync(string phone);
}
