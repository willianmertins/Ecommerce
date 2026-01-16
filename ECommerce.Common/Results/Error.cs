namespace ECommerce.Common.Results;

public sealed record Error
{
    public string Code { get; }
    public string Message { get; }

    private Error(string code, string message)
    {
        Code = code;
        Message = message;
    }

    public static readonly Error None 
        = new(string.Empty, string.Empty);

    public static readonly Error NullValue 
        = new("Error.NullValue", "Null value was provided");

    public static Error NotFound(string entity, Guid id)
        => new($"{entity}. NotFound", $"{entity} with id {id} was not found");

    public static Error Validation(string message)
        => new("Error.Validation", message);

    public static Error Failure(string code, string message)
        => new(code, message);
}
