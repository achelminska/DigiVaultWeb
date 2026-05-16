using DigiVault.PortalWWW.Models;

namespace DigiVault.PortalWWW.Services;

public class CmsService(ApiService api)
{
    public async Task<string> GetValueAsync(string key, string fallback = "")
    {
        try
        {
            var encodedKey = Uri.EscapeDataString(key);
            var item = await api.GetAsync<CmsContentDto>($"/api/cms/{encodedKey}");
            return string.IsNullOrWhiteSpace(item?.Value) ? fallback : item.Value;
        }
        catch
        {
            return fallback;
        }
    }
}
