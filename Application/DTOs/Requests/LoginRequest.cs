namespace Application.DTOs.Requests;

public class LoginRequest
{
    public string Provider { get; set; } = string.Empty; // "Google" or "Apple"
    public string IdToken { get; set; } = string.Empty;
}
