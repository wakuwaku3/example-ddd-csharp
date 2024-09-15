namespace Example.DDD.CSharp.Shared.DataBase;

public class DummyTransaction : ITransaction
{
    public Task CommitAsync()
    {
        return CommitAsync(() => { return Task.CompletedTask; });
    }

    public async Task CommitAsync(Func<Task> callbackAsync)
    {
        await callbackAsync();
    }

    public Task RollbackAsync()
    {
        return Task.CompletedTask;
    }
}
