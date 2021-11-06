using ExceptionHandling.CustomExceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Rollbar;

namespace ExceptionHandling
{
    public class GlobalErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                if (error is not CustomExceptionBase)
                {
                    RollbarLocator.RollbarInstance.Error(error);
                }
                HttpResponse response = context.Response;
                await WriteErrorMessageToResponse(response, error);
            }
        }

        private static async Task WriteErrorMessageToResponse(HttpResponse response, Exception error)
        {
            response.ContentType = "application/json";
            response.StatusCode = GetStatusCodeFromError(error);
            string message = error is CustomExceptionBase ? error.Message : "General Error";
            string result = JsonSerializer.Serialize(new {message});
            await response.WriteAsync(result);
        }

        private static int GetStatusCodeFromError(Exception error)
        {
            switch (error)
            {
                case CustomExceptionBase:
                    return (int) HttpStatusCode.BadRequest;
                case KeyNotFoundException:
                    return (int) HttpStatusCode.NotFound;
                default:
                    return (int) HttpStatusCode.InternalServerError;
            }
        }
    }
}