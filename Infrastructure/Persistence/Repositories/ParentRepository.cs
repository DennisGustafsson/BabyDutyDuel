using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence.EFC.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class ParentRepository : IParentRepository
{
    private readonly DataContext _context;

    public ParentRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<Parent?> GetByIdAsync(Guid id)
    {
        return await _context.Parents.FindAsync(id);
    }

    public async Task<Parent?> GetByExternalIdAsync(string externalId, string provider)
    {
        return await _context.Parents
            .FirstOrDefaultAsync(p => p.ExternalId == externalId && p.Provider == provider);
    }

    public async Task<Parent> CreateAsync(Parent parent)
    {
        await _context.Parents.AddAsync(parent);
        return parent;
    }

    public async Task UpdateAsync(Parent parent)
    {
        _context.Parents.Update(parent);
    }
}
