using Example.DDD.CSharp.Shared.Core.Returning;
using Example.DDD.CSharp.Shared.Core.ValueObject;

namespace Example.DDD.CSharp.Account.User;


internal record UserId(Id Id)
{
    internal static UserId New() => new(Id.New());
    internal Guid Value => Id.Value;
}

internal record UserVersionId(Id Id)
{
    internal static UserVersionId New() => new(Id.New());
    internal Guid Value => Id.Value;
}
internal record Name
{
    string Value { get; init; }
    public Name(string value)
    {
        Value = value;
        var errors = Validate(value);
        if (errors.Any())
        {
            throw new InvalidArgumentException("Invalid name", errors);
        }
    }
    private static IEnumerable<Error> Validate(string value)
    {
        if (value.Length < 3)
        {
            yield return new Error("Name is too short");
        }
    }
    public static IResult<Name> Parse(string value)
    {
        var errors = Validate(value);
        if (errors.Any())
        {
            return new Failure<Name>(errors);
        }

        return new Success<Name>(new(value));
    }
}

internal record UserEmail(Email Email)
{
    string Value => Email.Value;

    internal static IResult<UserEmail> Parse(string value)
    {
        var result = Email.Parse(value);
        return result switch
        {
            Success<Email> success => new Success<UserEmail>(new(success.Value)),
            Failure<Email> failure => new Failure<UserEmail>(failure.Errors),
            _ => throw new NotImplementedException()
        };
    }
}
internal record CreatedAt(DateTimeOffset Value)
{
    internal static CreatedAt Now() => new(DateTimeOffset.Now);
}

internal record RootEntity(UserId Id, UserVersionId VersionId, Name Name, UserEmail Email, CreatedAt CreatedAt)
{
    internal static CreatedEvent Create(Name name, UserEmail email)
    {
        return new CreatedEvent(new RootEntity(UserId.New(), UserVersionId.New(), name, email, CreatedAt.Now()));
    }
}
