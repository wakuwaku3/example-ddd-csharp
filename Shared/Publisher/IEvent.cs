namespace Example.DDD.CSharp.Shared.Publisher;

public interface IEvent<TPayload>
{
    TPayload Payload { get; }
}
