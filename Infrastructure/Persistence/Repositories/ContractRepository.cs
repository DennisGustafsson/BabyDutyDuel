using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence.EFC.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class ContractRepository : IContractRepository
{
    private readonly DataContext _context;

    public ContractRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<Contract?> GetByIdAsync(Guid id)
    {
        return await _context.Contracts.FindAsync(id);
    }

    public async Task<Contract> CreateAsync(Contract contract)
    {
        await _context.Contracts.AddAsync(contract);
        return contract;
    }

    public async Task<List<Contract>> GetByParentIdAsync(Guid parentId)
    {
        return await _context.Contracts
            .Where(c => c.Parent1Id == parentId || c.Parent2Id == parentId)
            .ToListAsync();
    }

    public async Task<List<Contract>> GetActiveContractsAsync()
    {
        return await _context.Contracts
            .Where(c => c.Status == ContractStatus.Active)
            .ToListAsync();
    }

    public async Task UpdateAsync(Contract contract)
    {
        _context.Contracts.Update(contract);
    }
}

