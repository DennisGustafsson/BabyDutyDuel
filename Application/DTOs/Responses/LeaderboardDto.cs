namespace Application.DTOs.Responses;

public class LeaderboardDto
{
    public Guid ContractId { get; set; }
    public List<ParentScoreDto> Scores { get; set; } = new();
}

public class ParentScoreDto
{
    public Guid ParentId { get; set; }
    public string ParentName { get; set; } = string.Empty;
    public int TotalPoints { get; set; }
    public int CompletedChores { get; set; }
}
