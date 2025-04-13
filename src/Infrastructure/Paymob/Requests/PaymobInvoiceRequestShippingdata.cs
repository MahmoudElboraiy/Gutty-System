using System.Text.Json.Serialization;

namespace Infrastructure.Paymob.Requests;

public class PaymobInvoiceRequestShippingdata
{
    [JsonPropertyName("first_name")]
    public string FirstName { get; set; } = null!;
    [JsonPropertyName("last_name")]
    public string LastName { get; set; } = null!;
    [JsonPropertyName("phone_number")]
    public string PhoneNumber { get; set; } = null!;
    [JsonPropertyName("email")]
    public string Email { get; set; } = null!; 
}