using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionHandling
{
    public static class GlobalErrorHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseGlobalErrorHandlerMiddleware(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GlobalErrorHandlerMiddleware>();
        }
    }
}
