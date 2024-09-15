namespace Example.DDD.CSharp.Shared.DataBase;

public class DummyTransactionManager : ITransactionManager
{
    public Task<ITransaction> BeginAsync()
    {
        return Task.FromResult<ITransaction>(new DummyTransaction());
    }
}
