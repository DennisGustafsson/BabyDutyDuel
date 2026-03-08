using Application.DTOs.Requests;
using Application.DTOs.Responses;
using Application.Interfaces.Repositories;
using Domain.Entities;

namespace Application.UseCases.BabyChores;

public class CompleteChoreUseCase
{
    private readonly IBabyChoreRepository _choreRepository;
    private readonly IChoreCompletionRepository _completionRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CompleteChoreUseCase(
        IBabyChoreRepository choreRepository,
        IChoreCompletionRepository completionRepository,
        IUnitOfWork unitOfWork)
    {
        _choreRepository = choreRepository;
        _completionRepository = completionRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ChoreCompletionDto> ExecuteAsync(CompleteChoreRequest request, Guid completedByParentId)
    {
        var chore = await _choreRepository.GetByIdAsync(request.ChoreId)
            ?? throw new InvalidOperationException($"Chore with ID {request.ChoreId} not found");

        var completion = ChoreCompletion.Create(
            chore.Id,
            completedByParentId,
            chore.PointValue,
            request.Notes
        );

        await _completionRepository.CreateAsync(completion);
        await _unitOfWork.SaveChangesAsync();

        return new ChoreCompletionDto
        {
            Id = completion.Id,
            ChoreId = completion.ChoreId,
            CompletedByParentId = completion.CompletedByParentId,
            CompletedAt = completion.CompletedAt.DateTime,
            PointsAwarded = completion.PointsAwarded,
            Notes = completion.Notes,
            IsVerified = completion.IsVerified
        };
    }
}
