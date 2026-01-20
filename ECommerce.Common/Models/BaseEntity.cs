namespace ECommerce.Common.Models;

public abstract class BaseEntity
{
    public Guid Id { get; protected set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; protected set; }

    private readonly List<BaseDomainEvents> _domainEvents = new();
    public IReadOnlyCollection<BaseDomainEvents> DomainEvents => _domainEvents.AsReadOnly();

    protected void AddDomainEvent(BaseDomainEvents eventItem) { _domainEvents.Add(eventItem); }
    public void ClearDomainEvents() { _domainEvents.Clear(); }
    protected void SetUpdatedAt() { UpdatedAt = DateTime.UtcNow; }
}
