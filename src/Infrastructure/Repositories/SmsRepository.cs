
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
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

    public async Task<bool> SendSmsAsync(string phoneNumber, string message)
    {
        var token = _config["WhySms:ApiToken"];
        var sender = _config["WhySms:SenderId"];
        var baseUrl = _config["WhySms:BaseUrl"];

        var url = $"{baseUrl}?recipient=+{phoneNumber}&sender_id={sender}&message={Uri.EscapeDataString(message)}&type=plain";

        var request = new HttpRequestMessage(HttpMethod.Post, url);
        request.Headers.Add("Authorization", $"Bearer {token}");

        var response = await _httpClient.SendAsync(request);

        return response.IsSuccessStatusCode;
    }
}