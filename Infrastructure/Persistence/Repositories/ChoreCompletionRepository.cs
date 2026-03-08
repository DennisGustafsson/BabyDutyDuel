using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence.EFC.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class ChoreCompletionRepository : IChoreCompletionRepository
{
    private readonly DataContext _context;

    public ChoreCompletionRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<ChoreCompletion?> GetByIdAsync(Guid id)
    {
        return await _context.ChoreCompletions.FindAsync(id);
    }

    public async Task<ChoreCompletion> CreateAsync(ChoreCompletion completion)
    {
        await _context.ChoreCompletions.AddAsync(completion);
        return completion;
    }

    public async Task<List<ChoreCompletion>> GetByChoreIdAsync(Guid choreId)
    {
        return await _context.ChoreCompletions
            .Where(c => c.ChoreId == choreId)
            .OrderByDescending(c => c.CompletedAt)
            .ToListAsync();
    }

    public async Task<List<ChoreCompletion>> GetByParentIdAsync(Guid parentId)
    {
        return await _context.ChoreCompletions
            .Where(c => c.CompletedByParentId == parentId)
            .OrderByDescending(c => c.CompletedAt)
            .ToListAsync();
    }

    public async Task<List<ChoreCompletion>> GetByContractIdAsync(Guid contractId)
    {
        return await _context.ChoreCompletions
            .Include(c => c)
            .Where(c => _context.BabyChores.Any(bc => bc.Id == c.ChoreId && bc.ContractId == contractId))
            .OrderByDescending(c => c.CompletedAt)
            .ToListAsync();
    }

    public async Task UpdateAsync(ChoreCompletion completion)
    {
        _context.ChoreCompletions.Update(completion);
    }
}
