namespace Example.DDD.CSharp.Shared.DataBase;

public interface ITransactionManager
{
    Task<ITransaction> BeginAsync();
}
