using System.Text.Json.Serialization;

namespace Infrastructure.Paymob.Responses;

public class PaymobQuickLinkResponse
{
    [JsonPropertyName("id")]
    public int Id { get; set; } 
    [JsonPropertyName("client_url")]
    public string ClientUrl { get; set; } = null!;
    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }
    [JsonPropertyName("order")]
    public int Order { get; set; } 
}