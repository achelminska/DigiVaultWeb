using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Json;

namespace DigiVault.Intranet.Services;

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
    SetAuthHeader();
    var response = await _client.GetAsync(endpoint);
    response.EnsureSuccessStatusCode();
    return await response.Content.ReadFromJsonAsync<T>();
}

public async Task PostAsync(string endpoint, object data)
{
    SetAuthHeader();
    var response = await _client.PostAsJsonAsync(endpoint, data);
    response.EnsureSuccessStatusCode();
}

public async Task PutAsync(string endpoint, object data)
{
    SetAuthHeader();
    var response = await _client.PutAsJsonAsync(endpoint, data);
    response.EnsureSuccessStatusCode();
}

public async Task PatchAsync(string endpoint, object data)
{
    SetAuthHeader();
    var response = await _client.PatchAsJsonAsync(endpoint, data);
    response.EnsureSuccessStatusCode();
}

public async Task DeleteAsync(string endpoint)
{
    SetAuthHeader();
    var response = await _client.DeleteAsync(endpoint);
    response.EnsureSuccessStatusCode();
}

private void SetAuthHeader()
{
    var token = _ctx.HttpContext?.Session.GetString("Token");
    _client.DefaultRequestHeaders.Authorization = 
        new AuthenticationHeaderValue("Bearer", token);
}
}

