namespace Example.DDD.CSharp.Shared.DataBase;

public interface ITransaction
{
    Task CommitAsync();
    Task CommitAsync(Func<Task> callbackAsync);
    Task RollbackAsync();
}
