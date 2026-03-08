using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface IParentRepository
{
    Task<Parent?> GetByIdAsync(Guid id);
    Task<Parent?> GetByExternalIdAsync(string externalId, string provider);
    Task<Parent> CreateAsync(Parent parent);
    Task UpdateAsync(Parent parent);
}
