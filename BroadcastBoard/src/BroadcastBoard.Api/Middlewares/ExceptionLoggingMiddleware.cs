using BroadcastBoard.Application.Common.Interfaces;
using BroadcastBoard.Application.Shows.Exceptions;
using BroadcastBoard.Infrastructure.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace BroadcastBoard.Api.Middleware
{
    public class ExceptionLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IErrorLogger _errorLogger;

        public ExceptionLoggingMiddleware(RequestDelegate next, IErrorLogger errorLogger)
        {
            _next = next;
            _errorLogger = errorLogger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ShowCollisionException ex)
            {
                _errorLogger.Log(ex.ToString());
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "application/json";
                var errorResponse = JsonSerializer.Serialize(new { error = ex.Message });
                await context.Response.WriteAsync(errorResponse);
            }
            catch (Exception ex)
            {
                _errorLogger.Log(ex.ToString());
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync("Internal server error");
            }
                }
    }
}
