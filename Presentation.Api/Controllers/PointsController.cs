using Application.DTOs.Responses;
using Application.UseCases.Points;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PointsController : ControllerBase
{
    private readonly GetLeaderboardUseCase _getLeaderboardUseCase;

    public PointsController(GetLeaderboardUseCase getLeaderboardUseCase)
    {
        _getLeaderboardUseCase = getLeaderboardUseCase;
    }

    [HttpGet("leaderboard/contract/{contractId}")]
    public async Task<ActionResult<LeaderboardDto>> GetLeaderboard(Guid contractId)
    {
        var result = await _getLeaderboardUseCase.ExecuteAsync(contractId);
        return Ok(result);
    }
}
