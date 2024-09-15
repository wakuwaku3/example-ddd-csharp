
namespace Example.DDD.CSharp.Shared.Publisher;

public class DummyPublisher : IPublisher
{
    public Task PublishAsync<TEvent, TEventPayload>(TEvent e) where TEvent : IEvent<TEventPayload>
    {
        try
        {
            // イベントを発行する
            Console.WriteLine($"Event {e.GetType().Name} published");
            return Task.CompletedTask;
        }
        catch (Exception ex)
        {
            // 復旧できるようにログを出力する
            Console.WriteLine($"Error publishing event {e.GetType().Name}: {ex.Message}");
            return Task.FromException(ex);
        }
    }
}
