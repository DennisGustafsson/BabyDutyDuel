namespace Application.Interfaces.Authentication;

public interface ITokenService
{
    string GenerateAccessToken(Guid userId, string email, string name);
    string GenerateRefreshToken();
    bool ValidateToken(string token);
    Guid? GetUserIdFromToken(string token);
}
