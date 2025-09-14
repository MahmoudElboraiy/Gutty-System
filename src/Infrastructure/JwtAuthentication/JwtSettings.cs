namespace Infrastructure.JwtAuthentication;

public class JwtSettings
{
    public string Key { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;

    public static JwtSettings Create()
    {
        var key = Environment.GetEnvironmentVariable("JWT_KEY") ?? string.Empty;
        var issuer = Environment.GetEnvironmentVariable("JWT_ISSUER") ?? string.Empty;
        var audience = Environment.GetEnvironmentVariable("JWT_AUDIENCE") ?? string.Empty;

        return new JwtSettings
        {
            Key = key,
            Issuer = issuer,
            Audience = audience,
        };
    }
}
