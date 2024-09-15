namespace Example.DDD.CSharp.Shared.Publisher;

public interface IPublisher
{
    Task PublishAsync<TEvent, TEventPayload>(TEvent e) where TEvent : IEvent<TEventPayload>;
}
