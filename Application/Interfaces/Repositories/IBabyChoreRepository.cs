using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface IBabyChoreRepository
{
    Task<BabyChore?> GetByIdAsync(Guid id);
    Task<BabyChore> CreateAsync(BabyChore chore);
    Task<List<BabyChore>> GetByContractIdAsync(Guid contractId);
    Task UpdateAsync(BabyChore chore);
    Task DeleteAsync(Guid id);
}
