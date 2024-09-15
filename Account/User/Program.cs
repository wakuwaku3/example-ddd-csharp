using Example.DDD.CSharp.Account.User;
using Example.DDD.CSharp.Shared.DataBase;
using Example.DDD.CSharp.Shared.Publisher;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<ITransactionManager, DummyTransactionManager>();
builder.Services.AddSingleton<IPublisher, DummyPublisher>();
builder.Services.AddSingleton<CreateUseCase>();
builder.Services.AddSingleton<Repository>();

var app = builder.Build();

app.UseAuthorization();

app.MapControllers();

app.Run();
