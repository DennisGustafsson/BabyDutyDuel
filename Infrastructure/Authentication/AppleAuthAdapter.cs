using Application.Interfaces.Authentication;

namespace Infrastructure.Authentication;

public class AppleAuthAdapter : IAppleAuthService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public AppleAuthAdapter(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<ExternalAuthResult> ValidateTokenAsync(string idToken)
    {
        // TODO: Implement Apple Sign In validation
        // Apple uses JWT tokens that need to be validated against Apple's public keys
        // Reference: https://developer.apple.com/documentation/sign_in_with_apple/sign_in_with_apple_rest_api/verifying_a_user
        
        await Task.CompletedTask;
        
        return new ExternalAuthResult 
        { 
            IsValid = false,
            Provider = "Apple"
        };
    }
}
