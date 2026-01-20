namespace ECommerce.Common.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message)
    {
    }
    
    public NotFoundException(string entity, Guid id) : base($"The entity '{entity}' with id '{id}' was not found.")
    {
    }
}