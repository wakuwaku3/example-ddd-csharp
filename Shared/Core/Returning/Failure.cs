namespace Example.DDD.CSharp.Shared.Core.Returning;

public class FailureException(IEnumerable<Error> errors) : Exception(string.Join(", ", errors))
{
    public IEnumerable<Error> Errors { get; } = errors;
}

public record Failure<T>(IEnumerable<Error> Errors) : IResult<T>
{
    public T Value => throw new FailureException(Errors);
}
