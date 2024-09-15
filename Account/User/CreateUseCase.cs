using Example.DDD.CSharp.Shared.Contract.Account.User;
using Example.DDD.CSharp.Shared.Core.Layered;
using Example.DDD.CSharp.Shared.Core.Returning;
using Example.DDD.CSharp.Shared.DataBase;
using Example.DDD.CSharp.Shared.Publisher;

namespace Example.DDD.CSharp.Account.User;

public record CreateInput(string Name, string Email)
{
    public CreateInput(AccountUserCreateRequest request) : this(request.Name, request.Email)
    {

    }
    internal IResult<Name> ConvertUserName => User.Name.Parse(Name);
    internal IResult<UserEmail> ConvertEmail => User.UserEmail.Parse(Email);
}

public record CreateOutput(Guid Id, Guid VersionId)
{
    internal CreateOutput(CreatedEvent e) : this(e.AfterChanged.Id.Value, e.AfterChanged.VersionId.Value) { }

    internal AccountUserCreateResponse Response => new(Id, VersionId);
}

public class CreateUseCase(
    ITransactionManager transactionManager,
    IPublisher publisher,
    Repository repository
) : IUseCase<CreateInput, CreateOutput>
{
    public async Task<IResult<CreateOutput>> ExecuteAsync(CreateInput input)
    {
        var transaction = await transactionManager.BeginAsync();
        try
        {
            // 検証を行う
            var name = input.ConvertUserName;
            if (name is Failure<Name> failureConvertName)
            {
                await transaction.RollbackAsync();
                return new Failure<CreateOutput>(failureConvertName.Errors);
            }
            var email = input.ConvertEmail;
            if (email is Failure<UserEmail> failureConvertEmail)
            {
                await transaction.RollbackAsync();
                return new Failure<CreateOutput>(failureConvertEmail.Errors);
            }

            // モデルを生成する
            var e = RootEntity.Create(name.Value, email.Value);

            // 変更したデータをデータベースに保存する
            await repository.CreateAsync(e);

            // コミットのコールバックでイベントを発行する
            await transaction.CommitAsync(async () => await publisher.PublishAsync<CreatedEvent, AccountUserCreatedEventPayload>(e));

            return new Success<CreateOutput>(new(e));
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}
