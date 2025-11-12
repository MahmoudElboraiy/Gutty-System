
namespace Application.Interfaces;

public interface ISmsRepository
{
    Task<string> SendSmsAsync(string phoneNumber, string message);
}
