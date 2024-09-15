using Example.DDD.CSharp.Shared.Core.Returning;

namespace Example.DDD.CSharp.Shared.Core.Layered;

public interface IUseCase<TInput, TOutput>
{
    Task<IResult<TOutput>> ExecuteAsync(TInput input);
}
