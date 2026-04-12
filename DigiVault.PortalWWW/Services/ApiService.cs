using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Json;

namespace DigiVault.PortalWWW.Services;

public class ApiService
{
    private readonly HttpClient _client;
    private readonly IHttpContextAccessor _ctx;

    public ApiService(IHttpClientFactory factory, IHttpContextAccessor ctx)
    {
        _client = factory.CreateClient("DigiVaultAPI");
        _ctx = ctx;
    }

    public async Task<T> GetAsync<T>(string endpoint)
    {
        var response = await _client.GetAsync(endpoint);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<T>();
    }

    public async Task<T> GetAuthAsync<T>(string endpoint)
    {
        var token = _ctx.HttpContext?.Session.GetString("Token");
        _client.DefaultRequestHeaders.Authorization = 
            new AuthenticationHeaderValue("Bearer", token);
        return await GetAsync<T>(endpoint);
    }

    public async Task<T> PostAsync<T>(string endpoint, object data)
    {
        var response = await _client.PostAsJsonAsync(endpoint, data);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<T>();
    }
}