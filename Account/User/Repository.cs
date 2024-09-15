namespace Example.DDD.CSharp.Account.User;

public class Repository
{
    internal async Task CreateAsync(CreatedEvent e)
    {
        // ユーザーを作成する
        await Task.CompletedTask;
    }
}
