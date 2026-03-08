using Application.DTOs.Requests;
using Application.DTOs.Responses;
using Application.Interfaces.Authentication;
using Application.Interfaces.Repositories;
using Domain.Entities;

namespace Application.UseCases.Authentication;

public class LoginWithAppleUseCase
{
    private readonly IAppleAuthService _appleAuthService;
    private readonly IParentRepository _parentRepository;
    private readonly ITokenService _tokenService;
    private readonly IUnitOfWork _unitOfWork;

    public LoginWithAppleUseCase(
        IAppleAuthService appleAuthService,
        IParentRepository parentRepository,
        ITokenService tokenService,
        IUnitOfWork unitOfWork)
    {
        _appleAuthService = appleAuthService;
        _parentRepository = parentRepository;
        _tokenService = tokenService;
        _unitOfWork = unitOfWork;
    }

    public async Task<AuthResponse> ExecuteAsync(LoginRequest request)
    {
        var authResult = await _appleAuthService.ValidateTokenAsync(request.IdToken);
        
        if (!authResult.IsValid)
            throw new UnauthorizedAccessException("Invalid Apple token");

        var existingParent = await _parentRepository.GetByExternalIdAsync(authResult.ExternalId, "Apple");

        Parent parent;
        if (existingParent == null)
        {
            parent = Parent.Create(authResult.ExternalId, "Apple", authResult.Name, authResult.Email);
            await _parentRepository.CreateAsync(parent);
            await _unitOfWork.SaveChangesAsync();
        }
        else
        {
            parent = existingParent;
        }

        var accessToken = _tokenService.GenerateAccessToken(parent.Id, parent.Email, parent.Name);
        var refreshToken = _tokenService.GenerateRefreshToken();

        return new AuthResponse
        {
            UserId = parent.Id,
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            User = new ParentDto
            {
                Id = parent.Id,
                Name = parent.Name,
                Email = parent.Email,
                Provider = parent.Provider
            }
        };
    }
}
