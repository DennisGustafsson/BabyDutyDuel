using Application.Interfaces.Authentication;
using System.Net.Http.Json;

namespace Infrastructure.Authentication;

public class GoogleAuthAdapter : IGoogleAuthService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private const string GoogleTokenInfoUrl = "https://oauth2.googleapis.com/tokeninfo";

    public GoogleAuthAdapter(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<ExternalAuthResult> ValidateTokenAsync(string idToken)
    {
        try
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"{GoogleTokenInfoUrl}?id_token={idToken}");

            if (!response.IsSuccessStatusCode)
            {
                return new ExternalAuthResult { IsValid = false };
            }

            var tokenInfo = await response.Content.ReadFromJsonAsync<GoogleTokenInfo>();

            if (tokenInfo == null)
            {
                return new ExternalAuthResult { IsValid = false };
            }

            return new ExternalAuthResult
            {
                ExternalId = tokenInfo.Sub ?? string.Empty,
                Email = tokenInfo.Email ?? string.Empty,
                Name = tokenInfo.Name ?? string.Empty,
                Provider = "Google",
                IsValid = true
            };
        }
        catch
        {
            return new ExternalAuthResult { IsValid = false };
        }
    }

    private class GoogleTokenInfo
    {
        public string? Sub { get; set; }
        public string? Email { get; set; }
        public string? Name { get; set; }
    }
}
