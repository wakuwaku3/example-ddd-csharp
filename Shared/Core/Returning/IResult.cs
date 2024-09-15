namespace Example.DDD.CSharp.Shared.Core.Returning;

public interface IResult<T>
{
    T Value { get; }
}
