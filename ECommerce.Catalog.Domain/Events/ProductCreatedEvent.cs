using ECommerce.Common.Models;

namespace ECommerce.Catalog.Domain.Events;

public record ProductCreatedEvent(Guid ProductId, string ProductName) : BaseDomainEvents;
