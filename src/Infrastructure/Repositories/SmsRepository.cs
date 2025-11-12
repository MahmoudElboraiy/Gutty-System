
using Application.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.Json;
using Vonage;
using Vonage.Messaging;
using Vonage.Request;
using static System.Net.Mime.MediaTypeNames;

namespace Infrastructure.Repositories;

public class SmsRepository: ISmsRepository
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _config;
    private readonly string _apiKey;
    private readonly string _apiSecret;
    public SmsRepository(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _config = config;
    }

    public async Task<string> SendSmsAsync(string phoneNumber, string message)
    {
      

        var payload = new
        {
            api_token = _config["WhySms:ApiToken"],
            recipient = phoneNumber,
            sender_id = _config["WhySms:SenderId"],
            type = "plain ",
            message = message
        };

        var json = JsonSerializer.Serialize(payload);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(_config["WhySms:BaseUrl"], content);
        var body = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            return "Failed to send SMS";
        }

        return  "SMS sent successfully"  ;

    }
}