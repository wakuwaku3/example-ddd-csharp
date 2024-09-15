namespace Example.DDD.CSharp.Shared.Core.Returning;

public class InvalidArgumentException(string name, IEnumerable<Error> errors) : Exception($"Invalid argument {name}, {string.Join(", ", errors)}")
{
    public string? ParamName { get; }
}
