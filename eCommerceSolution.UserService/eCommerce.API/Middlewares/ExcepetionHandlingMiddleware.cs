using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace eCommerce.API.Middlewares;

// You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
public class ExcepetionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExcepetionHandlingMiddleware> _logger;
    public ExcepetionHandlingMiddleware(RequestDelegate next, ILogger<ExcepetionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);

        }
        catch (Exception ex) 
        {
            _logger.LogError($"{ex.GetType().ToString()} : {ex.Message}");
            if (ex.InnerException is not null)
            {
                _logger.LogError($"{ex.InnerException.GetType().ToString()} : {ex.Message}");
            }
            httpContext.Response.StatusCode = 500; // Internal Server Error
            await httpContext.Response.WriteAsJsonAsync(new { Message = ex.Message, Type = ex.GetType().ToString()});
        }
    }
}

// Extension method used to add the middleware to the HTTP request pipeline.
public static class ExcepetionHandlingMiddlewareExtensions
{
    public static IApplicationBuilder UseExcepetionHandlingMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExcepetionHandlingMiddleware>();
    }
}