using Example.DDD.CSharp.Shared.Contract.Account.User;
using Example.DDD.CSharp.Shared.Publisher;

namespace Example.DDD.CSharp.Account.User;

internal record CreatedEvent(RootEntity AfterChanged) : IEvent<AccountUserCreatedEventPayload>
{
    public AccountUserCreatedEventPayload Payload { get; } = new AccountUserCreatedEventPayload(AfterChanged.Id.Value, AfterChanged.VersionId.Value);
}

