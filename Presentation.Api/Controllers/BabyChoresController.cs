using Application.DTOs.Requests;
using Application.DTOs.Responses;
using Application.UseCases.BabyChores;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BabyChoresController : ControllerBase
{
    private readonly CreateBabyChoreUseCase _createChoreUseCase;
    private readonly CompleteChoreUseCase _completeChoreUseCase;
    private readonly GetChoresByContractUseCase _getChoresByContractUseCase;

    public BabyChoresController(
        CreateBabyChoreUseCase createChoreUseCase,
        CompleteChoreUseCase completeChoreUseCase,
        GetChoresByContractUseCase getChoresByContractUseCase)
    {
        _createChoreUseCase = createChoreUseCase;
        _completeChoreUseCase = completeChoreUseCase;
        _getChoresByContractUseCase = getChoresByContractUseCase;
    }

    [HttpPost]
    public async Task<ActionResult<BabyChoreDto>> Create([FromBody] CreateBabyChoreRequest request)
    {
        // TODO: Get parent ID from authenticated user
        var createdByParentId = Guid.NewGuid(); // Temporary placeholder
        
        var result = await _createChoreUseCase.ExecuteAsync(request, createdByParentId);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPost("{id}/complete")]
    public async Task<ActionResult<ChoreCompletionDto>> Complete(Guid id, [FromBody] CompleteChoreRequest request)
    {
        // TODO: Get parent ID from authenticated user
        var completedByParentId = Guid.NewGuid(); // Temporary placeholder
        
        request.ChoreId = id;
        var result = await _completeChoreUseCase.ExecuteAsync(request, completedByParentId);
        return Ok(result);
    }

    [HttpGet("contract/{contractId}")]
    public async Task<ActionResult<List<BabyChoreDto>>> GetByContract(Guid contractId)
    {
        var result = await _getChoresByContractUseCase.ExecuteAsync(contractId);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BabyChoreDto>> GetById(Guid id)
    {
        // TODO: Implement GetBabyChoreByIdUseCase
        return NotFound();
    }
}
