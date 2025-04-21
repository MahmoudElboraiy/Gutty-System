using System.Text.Json.Serialization;

namespace Infrastructure.Paymob.Requests;

public class PaymobInvoiceRequest
{
    [JsonPropertyName("api_source")]
    public string ApiSource { get; set; } = "INVOICE";

    [JsonPropertyName("product_name")]
    public string ProductName { get; set; } = "Consultation Service";

    [JsonPropertyName("currency")]
    public string Currency { get; set; } = "EGP";

    [JsonPropertyName("amount_cents")]
    public int AmountCents { get; set; }

    [JsonPropertyName("integrations")]
    public int[] PaymentMethods { get; set; } = null!;

    [JsonPropertyName("shipping_data")]
    public PaymobInvoiceRequestShippingdata ShippingData { get; set; } = null!;
}
