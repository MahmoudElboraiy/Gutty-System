
using Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Vonage;
using Vonage.Messaging;
using Vonage.Request;
using static System.Net.Mime.MediaTypeNames;

namespace Infrastructure.Repositories;

public class SmsRepository: ISmsRepository
{
    private readonly string _apiKey;
    private readonly string _apiSecret;
    public SmsRepository(IConfiguration config)
    {
        _apiKey = config["Vonage:ApiKey"];
        _apiSecret = config["Vonage:ApiSecret"];
    }

    public async Task<bool> SendSmsAsync(string to, string message)
    {
        var creds = Credentials.FromApiKeyAndSecret(_apiKey, _apiSecret);
        var client = new VonageClient(creds);

        var response = await client.SmsClient.SendAnSmsAsync(new SendSmsRequest
        {
            To = to,
            From = "Gutty",
            Text = message
        });

        return response.Messages[0].Status == "0"; // "0" means success
    }
}
