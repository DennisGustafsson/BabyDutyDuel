using Application.DTOs.Responses;
using Application.Interfaces.Repositories;

namespace Application.UseCases.Points;

public class GetLeaderboardUseCase
{
    private readonly IChoreCompletionRepository _completionRepository;
    private readonly IContractRepository _contractRepository;
    private readonly IParentRepository _parentRepository;

    public GetLeaderboardUseCase(
        IChoreCompletionRepository completionRepository,
        IContractRepository contractRepository,
        IParentRepository parentRepository)
    {
        _completionRepository = completionRepository;
        _contractRepository = contractRepository;
        _parentRepository = parentRepository;
    }

    public async Task<LeaderboardDto> ExecuteAsync(Guid contractId)
    {
        var contract = await _contractRepository.GetByIdAsync(contractId)
            ?? throw new InvalidOperationException($"Contract with ID {contractId} not found");

        var completions = await _completionRepository.GetByContractIdAsync(contractId);

        var parent1 = await _parentRepository.GetByIdAsync(contract.Parent1Id);
        var parent2 = await _parentRepository.GetByIdAsync(contract.Parent2Id);

        var parent1Completions = completions.Where(c => c.CompletedByParentId == contract.Parent1Id).ToList();
        var parent2Completions = completions.Where(c => c.CompletedByParentId == contract.Parent2Id).ToList();

        return new LeaderboardDto
        {
            ContractId = contractId,
            Scores = new List<ParentScoreDto>
            {
                new ParentScoreDto
                {
                    ParentId = contract.Parent1Id,
                    ParentName = parent1?.Name ?? "Unknown",
                    TotalPoints = parent1Completions.Sum(c => c.PointsAwarded),
                    CompletedChores = parent1Completions.Count
                },
                new ParentScoreDto
                {
                    ParentId = contract.Parent2Id,
                    ParentName = parent2?.Name ?? "Unknown",
                    TotalPoints = parent2Completions.Sum(c => c.PointsAwarded),
                    CompletedChores = parent2Completions.Count
                }
            }.OrderByDescending(s => s.TotalPoints).ToList()
        };
    }
}
