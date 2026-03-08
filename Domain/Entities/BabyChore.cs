namespace Domain.Entities;

public class BabyChore
{
    public Guid Id { get; private set; }
    public Guid ContractId { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public int PointValue { get; private set; }
    public ChoreCategory Category { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }
    public Guid CreatedByParentId { get; private set; }

    private BabyChore() { }

    public static BabyChore Create(Guid contractId, string title, string description, int pointValue, ChoreCategory category, Guid createdByParentId)
    {
        if (contractId == Guid.Empty)
            throw new ArgumentException("ContractId cannot be empty", nameof(contractId));
        
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty", nameof(title));
        
        if (pointValue <= 0)
            throw new ArgumentException("Point value must be positive", nameof(pointValue));

        return new BabyChore
        {
            Id = Guid.NewGuid(),
            ContractId = contractId,
            Title = title,
            Description = description,
            PointValue = pointValue,
            Category = category,
            CreatedAt = DateTimeOffset.UtcNow,
            CreatedByParentId = createdByParentId
        };
    }
}

public enum ChoreCategory
{
    Feeding,
    Diapering,
    Sleeping,
    Bathing,
    Playing,
    Medical,
    Other
}
