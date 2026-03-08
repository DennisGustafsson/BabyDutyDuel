namespace Domain.Entities;

public class Parent
{
    public Guid Id { get; private set; }
    public string ExternalId { get; private set; } = string.Empty;
    public string Provider { get; private set; } = string.Empty;
    public string Name { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; private set; }

    private Parent() { }

    public static Parent Create(string externalId, string provider, string name, string email)
    {
        if (string.IsNullOrWhiteSpace(externalId))
            throw new ArgumentException("ExternalId cannot be empty", nameof(externalId));
        
        if (string.IsNullOrWhiteSpace(provider))
            throw new ArgumentException("Provider cannot be empty", nameof(provider));
        
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email cannot be empty", nameof(email));

        return new Parent
        {
            Id = Guid.NewGuid(),
            ExternalId = externalId,
            Provider = provider,
            Name = name,
            Email = email,
            CreatedAt = DateTimeOffset.UtcNow
        };
    }

    public void UpdateProfile(string name, string email)
    {
        if (!string.IsNullOrWhiteSpace(name))
            Name = name;
        
        if (!string.IsNullOrWhiteSpace(email))
            Email = email;
    }
}
