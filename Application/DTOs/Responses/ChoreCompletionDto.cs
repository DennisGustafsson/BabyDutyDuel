namespace Application.DTOs.Responses;

public class ChoreCompletionDto
{
    public Guid Id { get; set; }
    public Guid ChoreId { get; set; }
    public Guid CompletedByParentId { get; set; }
    public DateTime CompletedAt { get; set; }
    public int PointsAwarded { get; set; }
    public string? Notes { get; set; }
    public bool IsVerified { get; set; }
    public Guid? VerifiedByParentId { get; set; }
    public DateTime? VerifiedAt { get; set; }
}
