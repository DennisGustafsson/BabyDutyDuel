namespace Domain.Entities;

public class ChoreCompletion
{
    public Guid Id { get; private set; }
    public Guid ChoreId { get; private set; }
    public Guid CompletedByParentId { get; private set; }
    public DateTimeOffset CompletedAt { get; private set; }
    public int PointsAwarded { get; private set; }
    public string? Notes { get; private set; }
    public bool IsVerified { get; private set; }
    public Guid? VerifiedByParentId { get; private set; }
    public DateTimeOffset? VerifiedAt { get; private set; }

    private ChoreCompletion() { }

    public static ChoreCompletion Create(Guid choreId, Guid completedByParentId, int pointsAwarded, string? notes = null)
    {
        if (choreId == Guid.Empty)
            throw new ArgumentException("ChoreId cannot be empty", nameof(choreId));
        
        if (completedByParentId == Guid.Empty)
            throw new ArgumentException("CompletedByParentId cannot be empty", nameof(completedByParentId));
        
        if (pointsAwarded < 0)
            throw new ArgumentException("Points cannot be negative", nameof(pointsAwarded));

        return new ChoreCompletion
        {
            Id = Guid.NewGuid(),
            ChoreId = choreId,
            CompletedByParentId = completedByParentId,
            CompletedAt = DateTimeOffset.UtcNow,
            PointsAwarded = pointsAwarded,
            Notes = notes,
            IsVerified = false
        };
    }

    public void Verify(Guid verifiedByParentId)
    {
        if (IsVerified)
            throw new InvalidOperationException("Chore completion is already verified");
        
        if (verifiedByParentId == CompletedByParentId)
            throw new InvalidOperationException("Parent cannot verify their own completion");

        IsVerified = true;
        VerifiedByParentId = verifiedByParentId;
        VerifiedAt = DateTimeOffset.UtcNow;
    }
}
