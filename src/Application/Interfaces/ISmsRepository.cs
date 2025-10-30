
namespace Application.Interfaces;

public interface ISmsRepository
{
    Task<bool> SendSmsAsync(string to, string message);
}
