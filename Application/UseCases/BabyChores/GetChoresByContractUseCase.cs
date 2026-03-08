using Application.DTOs.Responses;
using Application.Interfaces.Repositories;

namespace Application.UseCases.BabyChores;

public class GetChoresByContractUseCase
{
    private readonly IBabyChoreRepository _choreRepository;

    public GetChoresByContractUseCase(IBabyChoreRepository choreRepository)
    {
        _choreRepository = choreRepository;
    }

    public async Task<List<BabyChoreDto>> ExecuteAsync(Guid contractId)
    {
        var chores = await _choreRepository.GetByContractIdAsync(contractId);

        return chores.Select(c => new BabyChoreDto
        {
            Id = c.Id,
            ContractId = c.ContractId,
            Title = c.Title,
            Description = c.Description,
            PointValue = c.PointValue,
            Category = c.Category.ToString(),
            CreatedAt = c.CreatedAt.DateTime,
            CreatedByParentId = c.CreatedByParentId
        }).ToList();
    }
}
