using System.Text.Json.Serialization;

namespace Infrastructure.Paymob.Requests;

public class PaymobQuickLinkRequest
{
    [JsonPropertyName("amount_cents")]
    public int AmountCents { get; set; }
    [JsonPropertyName("payment_methods")]
    public int[] PaymentMethods { get; set; } = null!;
    [JsonPropertyName("is_live")]
    public bool IsLive { get; set; } = false;
    [JsonPropertyName("full_name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string FullName { get; set; } = null!;
    [JsonPropertyName("email")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Email { get; set; } = null!;
    [JsonPropertyName("phone_number")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Phone { get; set; } = null!;
    [JsonPropertyName("redirection_url")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string RedirectionUrl { get; set; } = null!;
    [JsonPropertyName("notification_url")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string NotificationUrl { get; set; } = null!; 
}