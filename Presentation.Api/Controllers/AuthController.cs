using Application.DTOs.Requests;
using Application.DTOs.Responses;
using Application.UseCases.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly LoginWithGoogleUseCase _loginWithGoogleUseCase;
    private readonly LoginWithAppleUseCase _loginWithAppleUseCase;

    public AuthController(
        LoginWithGoogleUseCase loginWithGoogleUseCase,
        LoginWithAppleUseCase loginWithAppleUseCase)
    {
        _loginWithGoogleUseCase = loginWithGoogleUseCase;
        _loginWithAppleUseCase = loginWithAppleUseCase;
    }

    [HttpPost("login/google")]
    public async Task<ActionResult<AuthResponse>> LoginWithGoogle([FromBody] LoginRequest request)
    {
        try
        {
            var result = await _loginWithGoogleUseCase.ExecuteAsync(request);
            return Ok(result);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
    }

    [HttpPost("login/apple")]
    public async Task<ActionResult<AuthResponse>> LoginWithApple([FromBody] LoginRequest request)
    {
        try
        {
            var result = await _loginWithAppleUseCase.ExecuteAsync(request);
            return Ok(result);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
    }
}
