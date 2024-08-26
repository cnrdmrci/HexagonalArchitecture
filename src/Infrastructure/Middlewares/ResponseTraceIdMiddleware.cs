using System.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Middlewares;

public class ResponseTraceIdMiddleware
{
    private readonly RequestDelegate _next;

    public ResponseTraceIdMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var traceId = Activity.Current?.TraceId.ToString();
        if (!string.IsNullOrEmpty(traceId))
        {
            context.Response.Headers["trace-id"] = traceId;
        }
        
        await _next(context);
    }
}