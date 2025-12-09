
namespace Application.Interfaces;

public interface ISmsRepository
{
    Task<bool> SendSmsAsync(string phoneNumber, string message);
}
