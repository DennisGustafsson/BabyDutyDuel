namespace Application.DTOs.Requests;

public class CompleteChoreRequest
{
    public Guid ChoreId { get; set; }
    public string? Notes { get; set; }
}
