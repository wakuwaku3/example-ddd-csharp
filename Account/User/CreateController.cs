using Example.DDD.CSharp.Shared.Contract.Account.User;
using Example.DDD.CSharp.Shared.Core.Returning;

using Microsoft.AspNetCore.Mvc;

namespace Example.DDD.CSharp.Account.User;

[ApiController]
[Route("api/account/user/create")]
public class CreateController(CreateUseCase useCase) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<AccountUserCreateResponse>> PostAsync(AccountUserCreateRequest request)
    {
        var output = await useCase.ExecuteAsync(new(request));
        return output switch
        {
            Success<CreateOutput> success => new CreatedResult(GetType().FullName, success.Value.Response),
            Failure<CreateOutput> failure => BadRequest(failure.Errors),
            _ => throw new NotImplementedException(),
        };
    }
}
