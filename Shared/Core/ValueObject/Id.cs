namespace Example.DDD.CSharp.Shared.Core.ValueObject;

public record Id
{
    public Guid Value { get; init; }

    public Id(Guid value)
    {
        Value = value;
    }

    public static Id New() => new(Guid.NewGuid());
}
