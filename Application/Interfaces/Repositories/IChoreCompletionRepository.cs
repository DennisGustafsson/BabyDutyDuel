using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface IChoreCompletionRepository
{
    Task<ChoreCompletion?> GetByIdAsync(Guid id);
    Task<ChoreCompletion> CreateAsync(ChoreCompletion completion);
    Task<List<ChoreCompletion>> GetByChoreIdAsync(Guid choreId);
    Task<List<ChoreCompletion>> GetByParentIdAsync(Guid parentId);
    Task<List<ChoreCompletion>> GetByContractIdAsync(Guid contractId);
    Task UpdateAsync(ChoreCompletion completion);
}
