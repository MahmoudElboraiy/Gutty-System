using System.Text;
using System.Text.Json;
using Infrastructure.Paymob.Requests;
using Infrastructure.Paymob.Responses;

namespace Infrastructure.Paymob;

public class PaymobPayment
{
    private readonly string _token;

    public PaymobPayment()
    {
        _token = this.GetToken("").Result;
    }

    public async Task<PaymobQuickLinkResponse> RequestQuickPaymentLink(
        string fullName,
        string email,
        string phone,
        int price
    )
    {
        var Http = new HttpClient();

        var content = new PaymobQuickLinkRequest()
        {
            AmountCents = price * 100,
            PaymentMethods = new int[] { 4838552, 4838551 },
            IsLive = true,
            FullName = fullName,
            Email = email,
            Phone = "+2" + phone,
            NotificationUrl = "https://api.2l2ana.com/api/PaymobRequests",
            RedirectionUrl = "https://2l2ana.com/ar/my-profile",
        };

        var contentSerilized = JsonSerializer.Serialize(content);

        var request = new HttpRequestMessage
        {
            RequestUri = new Uri("https://accept.paymob.com/api/ecommerce/payment-links"),
            Method = HttpMethod.Post,
            Content = new StringContent(contentSerilized, Encoding.UTF8, "application/json"),
        };

        request.Headers.Add("Authorization", "Bearer " + _token);

        var HttpResponse = await Http.SendAsync(request);

        if (HttpResponse.IsSuccessStatusCode)
        {
            var responseContent = await HttpResponse.Content.ReadAsStringAsync();
            var response = JsonSerializer.Deserialize<PaymobQuickLinkResponse>(responseContent);
            return response;
        }
        else
        {
            var responseContent = await HttpResponse.Content.ReadAsStringAsync();
            throw new Exception(responseContent);
        }
    }

    private async Task<string> GetToken(string ApiKey)
    {
        var Http = new HttpClient();
        var request = new HttpRequestMessage
        {
            RequestUri = new Uri("https://accept.paymob.com/api/auth/tokens"),
            Method = HttpMethod.Post,
            Content = new StringContent(
                "{\"api_key\":\"" + ApiKey + "\"}",
                Encoding.UTF8,
                "application/json"
            ),
        };

        var HttpResponse = await Http.SendAsync(request);

        if (HttpResponse.IsSuccessStatusCode)
        {
            var responseContent = await HttpResponse.Content.ReadAsStringAsync();
            var response = JsonSerializer.Deserialize<GetTokenResponse>(responseContent);
            return response.Token;
        }
        else
        {
            throw new Exception("Failed to get token");
        }
    }
}
