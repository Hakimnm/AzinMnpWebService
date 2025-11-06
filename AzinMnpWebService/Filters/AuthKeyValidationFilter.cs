using AzinMnpWebService.Repositories;
using AzinMnpWebService.Services.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AzinMnpWebService.Filters;


public class AuthKeyValidationFilter : IAsyncActionFilter
{
    private readonly IAuthKeyService _authKeyService;

    public AuthKeyValidationFilter(IAuthKeyService authKeyService)
    {
        _authKeyService = authKeyService;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.HttpContext.Request.Headers.TryGetValue("Consumer-Key", out var keyValue))
        {
            context.Result = new UnauthorizedObjectResult("AuthKey notfound header");
            return;
        }

        var authKey = await _authKeyService.ValidateAuthKeyAsync(keyValue!);

        if (!authKey)
        {
            context.Result = new UnauthorizedObjectResult("Invalid authKey");
            return;
        }

        await next();
    }
}
