namespace ECommerce.Common.Models;

public abstract record BaseDomainEvents
{
    public Guid EventId { get; set; } = Guid.NewGuid();
    public DateTime OccureedOn { get; set; } = DateTime.UtcNow;
}
