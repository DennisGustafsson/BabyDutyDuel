namespace Application.DTOs.Requests;

public class CreateContractRequest
{
    public Guid Parent1Id { get; set; }
    public Guid Parent2Id { get; set; }
    public DateTime StartDate { get; set; }
}
