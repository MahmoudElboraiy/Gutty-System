using Domain.Models.Identity;

namespace Application.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user, string role, bool longExpires = false);
}