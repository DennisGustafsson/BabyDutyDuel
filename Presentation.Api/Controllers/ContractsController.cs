using Application.DTOs.Requests;
using Application.DTOs.Responses;
using Application.UseCases.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContractsController : ControllerBase
{
    private readonly CreateContractUseCase _createContractUseCase;
    private readonly GetContractByIdUseCase _getContractByIdUseCase;

    public ContractsController(
        CreateContractUseCase createContractUseCase,
        GetContractByIdUseCase getContractByIdUseCase)
    {
        _createContractUseCase = createContractUseCase;
        _getContractByIdUseCase = getContractByIdUseCase;
    }

    [HttpPost]
    public async Task<ActionResult<ContractDto>> Create([FromBody] CreateContractRequest request)
    {
        var result = await _createContractUseCase.ExecuteAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ContractDto>> GetById(Guid id)
    {
        var result = await _getContractByIdUseCase.ExecuteAsync(id);
        if (result == null)
            return NotFound();

        return Ok(result);
    }
}

