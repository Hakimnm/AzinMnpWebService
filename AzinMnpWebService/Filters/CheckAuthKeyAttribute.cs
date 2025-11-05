// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Mvc.Filters;
// using Microsoft.EntityFrameworkCore;
// namespace AzinMnpWebService.Filters;
//
//
//
// public class CheckAuthKeyAttribute : Attribute, IAsyncActionFilter
// {
//       public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
//     // {
//     //     var dbContext = context.HttpContext.RequestServices.GetRequiredService<AppDbContext>();
//     //     var headers = context.HttpContext.Request.Headers;
//     //
//     //     if (!headers.TryGetValue("X-Bridge-AuthorizationKey", out var authKey) || string.IsNullOrEmpty(authKey))
//     //     {
//     //         context.Result = new UnauthorizedObjectResult("Missing X-Bridge-AuthorizationKey header.");
//     //         return;
//     //     }
//     //
//     //     var isValid = await dbContext.ApiKeys.AnyAsync(k => k.Key == authKey && k.IsActive);
//     //     if (!isValid)
//     //     {
//     //         context.Result = new ObjectResult("Invalid or inactive authorization key.") { StatusCode = 403 };
//     //         return;
//     //     }
//     //
//     //     // Controllerdə istifadə üçün saxla
//     //     context.HttpContext.Items["AuthKey"] = authKey.ToString();
//     //
//           await next();
//      }
// }
