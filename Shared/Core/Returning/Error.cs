namespace Example.DDD.CSharp.Shared.Core.Returning;
public record Error(string Code, params object[] Args);
