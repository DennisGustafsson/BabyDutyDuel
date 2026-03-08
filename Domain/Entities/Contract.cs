namespace Domain.Entities;

public class Contract
{
    public Guid Id { get; private set; }
    public Guid Parent1Id { get; private set; }
    public Guid Parent2Id { get; private set; }
    public DateTimeOffset StartDate { get; private set; }
    public DateTimeOffset? EndDate { get; private set; }
    public ContractStatus Status { get; private set; }

    private Contract() { }

    public static Contract Create(Guid parent1Id, Guid parent2Id)
    {
        if (parent1Id == Guid.Empty)
            throw new ArgumentException("Parent1Id cannot be empty", nameof(parent1Id));

        if (parent2Id == Guid.Empty)
            throw new ArgumentException("Parent2Id cannot be empty", nameof(parent2Id));

        if (parent1Id == parent2Id)
            throw new ArgumentException("Parents cannot be the same person");

        return new Contract
        {
            Id = Guid.NewGuid(),
            Parent1Id = parent1Id,
            Parent2Id = parent2Id,
            StartDate = DateTimeOffset.UtcNow,
            Status = ContractStatus.Active
        };
    }

    public void End()
    {
        if (Status == ContractStatus.Ended)
            throw new InvalidOperationException("Contract is already ended");

        EndDate = DateTimeOffset.UtcNow;
        Status = ContractStatus.Ended;
    }
}

public enum ContractStatus
{
    Active,
    Ended
}

