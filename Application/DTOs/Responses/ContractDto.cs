namespace Application.DTOs.Responses;

public class ContractDto
{
    public Guid Id { get; set; }
    public Guid Parent1Id { get; set; }
    public Guid Parent2Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Status { get; set; } = string.Empty;
}
