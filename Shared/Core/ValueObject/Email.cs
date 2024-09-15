using Example.DDD.CSharp.Shared.Core.Returning;

namespace Example.DDD.CSharp.Shared.Core.ValueObject;

public record Email
{
    public string Value { get; init; }
    public Email(string value)
    {
        Value = value;
        var errors = Validate(value);
        if (errors.Any())
        {
            throw new InvalidArgumentException("Invalid email", errors);
        }
    }
    private static IEnumerable<Error> Validate(string value)
    {
        if (value.Length < 5)
        {
            yield return new Error("Email is too short");
        }
        if (!value.Contains('@'))
        {
            yield return new Error("Email is missing @");
        }
    }
    public static IResult<Email> Parse(string value)
    {
        var errors = Validate(value);
        if (errors.Any())
        {
            return new Failure<Email>(errors);
        }

        return new Success<Email>(new(value));
    }
}
