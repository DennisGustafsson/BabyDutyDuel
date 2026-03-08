using Application.DTOs.Responses;
using Application.Interfaces.Repositories;

namespace Application.UseCases.Contracts;

public class GetContractByIdUseCase
{
    private readonly IContractRepository _contractRepository;

    public GetContractByIdUseCase(IContractRepository contractRepository)
    {
        _contractRepository = contractRepository;
    }

    public async Task<ContractDto?> ExecuteAsync(Guid id)
    {
        var contract = await _contractRepository.GetByIdAsync(id);
        
        if (contract == null)
            return null;

        return new ContractDto
        {
            Id = contract.Id,
            Parent1Id = contract.Parent1Id,
            Parent2Id = contract.Parent2Id,
            StartDate = contract.StartDate.DateTime,
            Status = contract.Status.ToString()
        };
    }
}
