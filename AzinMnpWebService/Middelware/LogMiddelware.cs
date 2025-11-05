using System.Runtime.ExceptionServices;
using AzinMnpWebService.Models.Request;
using AzinMnpWebService.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;

namespace AzinMnpWebService.Middelware;

public class LogMiddelware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public LogMiddelware(RequestDelegate next, ILogger<LogMiddelware> logger)
    {
        _next = next;
        _logger = logger;
    }
    public async Task Invoke(HttpContext context, LoggingService loggingService)
    {
      var originalBodyStream = context.Response.Body;

      await using var memoryStream = new MemoryStream();
      context.Response.Body = memoryStream;

      // Save request log
      var requestLog = await loggingService.SaveRequestLog(context);
      context.Response.Headers["X-Trace-Id"] = context.TraceIdentifier = requestLog.Id.ToString();

      await _next(context);

      memoryStream.Seek(0, SeekOrigin.Begin);
      var responseBodyText = await new StreamReader(memoryStream).ReadToEndAsync();

      const string responseTime = "ResponseTime";
      requestLog.RequestDuration = context.Response.Headers[responseTime].ToString();
      context.Response.Headers.Remove(responseTime);

      await loggingService.SaveResponseLog(context.Response, requestLog, responseBodyText);

      memoryStream.Seek(0, SeekOrigin.Begin);
      await memoryStream.CopyToAsync(originalBodyStream);
      context.Response.Body = originalBodyStream;
    }

}