using System.Text;
using AzinMnpWebService.Models.Request;
using AzinMnpWebService.Repositories;

namespace AzinMnpWebService.Services;

public class LoggingService 
{
    private readonly string _requestLogCollection;

    private readonly IMongoRepository _mongoRepository;
    private readonly ILogger<LoggingService> _logger;

    public LoggingService(IConfiguration configuration, IMongoRepository mongoRepository,
        ILogger<LoggingService> logger)
    {
        _mongoRepository = mongoRepository;
        _logger = logger;
        _requestLogCollection = "request_logs";
    }

    public async Task<RequestLog> SaveRequestLog(HttpContext httpContext)
    {
        var body = await GetRequestBody(httpContext);

        var request = httpContext.Request;
        var formattedRequest = $"{request.Scheme}://{request.Host}{request.Path}{request.QueryString}{body}";

        var headers = new StringBuilder();
        foreach (var header in request.Headers)
        {
            headers.Append(header.Key).Append(':').AppendLine(header.Value);
        }

        RequestLog requestLog = new()
        {
            RequestUrl = formattedRequest,
            RequestHeaders = headers.ToString(),
            RequestIp = request.HttpContext.Connection.RemoteIpAddress?.ToString()
        };

        return await _mongoRepository.InsertRecordAsync(_requestLogCollection, requestLog);
    }

    private async Task<string> GetRequestBody(HttpContext httpContext)
    {
        
        
        if (!httpContext.Request.ContentLength.HasValue)
            return string.Empty;

        httpContext.Request.EnableBuffering();
        using (var ms = new MemoryStream())
        {
            await httpContext.Request.Body.CopyToAsync(ms);
            httpContext.Request.Body.Seek(0, SeekOrigin.Begin);
            ms.Seek(0, SeekOrigin.Begin);
            var requestBodyString = Encoding.UTF8.GetString(ms.ToArray());
            return requestBodyString;
        }
    }

    public async Task SaveResponseLog(HttpResponse response, RequestLog requestLog, string responseBody)
    {
        try
        {
            requestLog.ResponseStatusCode = response.StatusCode;
            requestLog.ResponseBody = responseBody;

            var headers = new StringBuilder();
            foreach (var header in response.Headers)
            {
                headers.Append(header.Key).Append(':').AppendLine(header.Value);
            }

            requestLog.ResponseHeaders = headers.ToString();
            await _mongoRepository.UpsertRecordAsync(_requestLogCollection, requestLog.Id, requestLog);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
        }
    }
}