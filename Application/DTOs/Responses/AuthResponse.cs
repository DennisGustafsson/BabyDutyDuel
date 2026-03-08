namespace Application.DTOs.Responses;

public class AuthResponse
{
    public Guid UserId { get; set; }
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public ParentDto User { get; set; } = new();
}
