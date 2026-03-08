using Application.DTOs.Requests;
using Application.DTOs.Responses;
using Application.Interfaces.Repositories;
using Domain.Entities;

namespace Application.UseCases.BabyChores;

public class CreateBabyChoreUseCase
{
    private readonly IBabyChoreRepository _choreRepository;
    private readonly IContractRepository _contractRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateBabyChoreUseCase(
        IBabyChoreRepository choreRepository,
        IContractRepository contractRepository,
        IUnitOfWork unitOfWork)
    {
        _choreRepository = choreRepository;
        _contractRepository = contractRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<BabyChoreDto> ExecuteAsync(CreateBabyChoreRequest request, Guid createdByParentId)
    {
        var contract = await _contractRepository.GetByIdAsync(request.ContractId)
            ?? throw new InvalidOperationException($"Contract with ID {request.ContractId} not found");

        if (contract.Status != ContractStatus.Active)
            throw new InvalidOperationException("Cannot create chore for inactive contract");

        var chore = BabyChore.Create(
            request.ContractId,
            request.Title,
            request.Description,
            request.PointValue,
            request.Category,
            createdByParentId
        );

        await _choreRepository.CreateAsync(chore);
        await _unitOfWork.SaveChangesAsync();

        return new BabyChoreDto
        {
            Id = chore.Id,
            ContractId = chore.ContractId,
            Title = chore.Title,
            Description = chore.Description,
            PointValue = chore.PointValue,
            Category = chore.Category.ToString(),
            CreatedAt = chore.CreatedAt.DateTime,
            CreatedByParentId = chore.CreatedByParentId
        };
    }
}
