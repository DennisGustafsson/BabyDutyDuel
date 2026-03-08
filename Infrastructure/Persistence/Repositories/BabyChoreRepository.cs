using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence.EFC.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class BabyChoreRepository : IBabyChoreRepository
{
    private readonly DataContext _context;

    public BabyChoreRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<BabyChore?> GetByIdAsync(Guid id)
    {
        return await _context.BabyChores.FindAsync(id);
    }

    public async Task<BabyChore> CreateAsync(BabyChore chore)
    {
        await _context.BabyChores.AddAsync(chore);
        return chore;
    }

    public async Task<List<BabyChore>> GetByContractIdAsync(Guid contractId)
    {
        return await _context.BabyChores
            .Where(c => c.ContractId == contractId)
            .OrderByDescending(c => c.CreatedAt)
            .ToListAsync();
    }

    public async Task UpdateAsync(BabyChore chore)
    {
        _context.BabyChores.Update(chore);
    }

    public async Task DeleteAsync(Guid id)
    {
        var chore = await _context.BabyChores.FindAsync(id);
        if (chore != null)
        {
            _context.BabyChores.Remove(chore);
        }
    }
}
