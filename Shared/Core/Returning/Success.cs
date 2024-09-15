namespace Example.DDD.CSharp.Shared.Core.Returning;

public record Success<T>(T Value) : IResult<T>;
