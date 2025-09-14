using System.Text.Json.Serialization;

namespace Infrastructure.Paymob.Responses;

public class GetTokenResponse
{
    [JsonPropertyName("token")]
    public string Token { get; set; } = null!;
}
