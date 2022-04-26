using Newtonsoft.Json;
using System.Net;
using Xero.Products.App.Http;

namespace Xero.Products.App.Middleware;
public class ErrorHandlerMiddleware
{
    readonly RequestDelegate _next;
    readonly IWebHostEnvironment _env;
    readonly ILogger _logger;

    public ErrorHandlerMiddleware(RequestDelegate next, IWebHostEnvironment env, ILoggerFactory loggerFactory)
    {
        _next = next;
        _env = env;
        _logger = loggerFactory.CreateLogger(nameof(ErrorHandlerMiddleware));
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (HttpException ex) when (ex.IsErrorMiddlewareTarget)
        {
            await WriteHttpException(context.Response, ex);
        }
        catch (Exception ex)
        {
            await WriteServerError(context.Response, ex);
        }
    }

    async Task WriteHttpException(HttpResponse response, HttpException ex)
    {
        if (response.HasStarted)
            return;

        if (ex.StatusCode == HttpStatusCode.InternalServerError)
        {
            await WriteServerError(response, ex);
            return;
        }

        response.StatusCode = (int)ex.StatusCode;
        if (!string.IsNullOrEmpty(ex.Message))
        {
            await response.WriteAsync(ex.Message);
        }
    }

    async Task WriteServerError(HttpResponse response, Exception ex)
    {
        _logger.LogError($"Global error:\n{ex.Message}\n{ex.InnerException?.Message}", ex);

        if (response.HasStarted)
            return;

        response.StatusCode = 500;
        if (_env.EnvironmentName != Environments.Production)
        {
            var error = JsonConvert.SerializeObject(ex, Formatting.Indented);
            await response.WriteAsync(error);
        }
    }
}
