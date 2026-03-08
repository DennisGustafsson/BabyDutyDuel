namespace Application.Interfaces.Authentication;

public interface IGoogleAuthService
{
    Task<ExternalAuthResult> ValidateTokenAsync(string idToken);
}

public interface IAppleAuthService
{
    Task<ExternalAuthResult> ValidateTokenAsync(string idToken);
}

public class ExternalAuthResult
{
    public string ExternalId { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Provider { get; set; } = string.Empty;
    public bool IsValid { get; set; }
}
