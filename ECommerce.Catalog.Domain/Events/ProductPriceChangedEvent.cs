using ECommerce.Common.Models;

namespace ECommerce.Catalog.Domain.Events;

public record ProductPriceChangedEvent(Guid ProductId, decimal OldPrice, decimal NewPrice)
    : BaseDomainEvents;
