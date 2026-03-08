using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface IContractRepository
{
    Task<Contract?> GetByIdAsync(Guid id);
    Task<Contract> CreateAsync(Contract contract);
    Task<List<Contract>> GetByParentIdAsync(Guid parentId);
    Task<List<Contract>> GetActiveContractsAsync();
    Task UpdateAsync(Contract contract);
}
