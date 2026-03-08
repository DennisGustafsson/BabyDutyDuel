using Application.DTOs.Requests;
using Application.DTOs.Responses;
using Application.Interfaces.Repositories;
using Domain.Entities;

namespace Application.UseCases.Contracts;

public class CreateContractUseCase
{
    private readonly IContractRepository _contractRepository;
    private readonly IParentRepository _parentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateContractUseCase(
        IContractRepository contractRepository,
        IParentRepository parentRepository,
        IUnitOfWork unitOfWork)
    {
        _contractRepository = contractRepository;
        _parentRepository = parentRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ContractDto> ExecuteAsync(CreateContractRequest request)
    {
        var parent1 = await _parentRepository.GetByIdAsync(request.Parent1Id)
            ?? throw new InvalidOperationException($"Parent with ID {request.Parent1Id} not found");

        var parent2 = await _parentRepository.GetByIdAsync(request.Parent2Id)
            ?? throw new InvalidOperationException($"Parent with ID {request.Parent2Id} not found");

        var contract = Contract.Create(request.Parent1Id, request.Parent2Id);

        await _contractRepository.CreateAsync(contract);
        await _unitOfWork.SaveChangesAsync();

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

