using System.Data.SqlClient;

using Microsoft.Extensions.Hosting;

namespace Example.DDD.CSharp.Account.User;

public class Tests
{
    [Test]
    public void ShouldSuccess()
    {
        // 環境変数を取得
        var host = Environment.GetEnvironmentVariable("TEST_DB_HOST");
        var port = Environment.GetEnvironmentVariable("TEST_DB_FORWARDING_PORT");
        var user = Environment.GetEnvironmentVariable("TEST_DB_USER");
        var password = Environment.GetEnvironmentVariable("TEST_DB_PASSWORD");
        var name = Environment.GetEnvironmentVariable("TEST_DB_NAME");
        var connectionString = new SqlConnectionStringBuilder
        {
            DataSource = $"{host},{port}",
            InitialCatalog = name,
            UserID = user,
            Password = password,
        }.ToString();

        // SQL Server に接続できることを確認する
        using var connection = new SqlConnection(connectionString);
        connection.Open();

        Assert.Pass();
    }
}
