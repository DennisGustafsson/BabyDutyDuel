using Domain.Entities;

namespace Application.DTOs.Requests;

public class CreateBabyChoreRequest
{
    public Guid ContractId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int PointValue { get; set; }
    public ChoreCategory Category { get; set; }
}
