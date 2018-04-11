using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Gadget.Web.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Logging;

namespace Gadget.Web.Middlewares
{
    public class LogRequestMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public LogRequestMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;

            _logger = loggerFactory.CreateLogger<LogRequestMiddleware>();
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.IsSwaggerRequest())
            {
                await _next(context);

                return;
            }

            #region request

            Guid requestId = Guid.NewGuid();

            StringBuilder logContext = new StringBuilder();

            logContext.AppendLine($"requestId={requestId}@{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}");

            logContext.AppendLine($"requestUrl={context.Request.GetDisplayUrl()}");

            foreach (var header in context.Request.Headers)
            {
                logContext.AppendLine($"header.{header.Key}={header.Value}");
            }

            foreach (var pair in context.Request.Query)
            {
                logContext.AppendLine($"query.{pair.Key}={pair.Value}");
            }

            if (!context.Request.Method.Equals("GET", StringComparison.CurrentCultureIgnoreCase))
            {
                var tempRequestBodyStream = new MemoryStream();

                await context.Request.Body.CopyToAsync(tempRequestBodyStream);

                tempRequestBodyStream.Seek(0, SeekOrigin.Begin);

                logContext.AppendLine($"request.Body={new StreamReader(tempRequestBodyStream).ReadToEnd()}");

                tempRequestBodyStream.Seek(0, SeekOrigin.Begin);

                context.Request.Body = tempRequestBodyStream;
            }

            _logger.LogInformation(logContext.ToString());

            #endregion

            var originalBodyStream = context.Response.Body;

            var tempResponseBodyStream = new MemoryStream();

            context.Response.Body = tempResponseBodyStream;
            
            await _next(context);

            #region  response

            tempResponseBodyStream.Seek(0, SeekOrigin.Begin);

            logContext.Clear();
            logContext.AppendLine($"requestId={requestId}@{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}");
            logContext.AppendLine($"response.Body={new StreamReader(tempResponseBodyStream).ReadToEnd()}");

            _logger.LogInformation(logContext.ToString());

            tempResponseBodyStream.Seek(0, SeekOrigin.Begin);
            
            await tempResponseBodyStream.CopyToAsync(originalBodyStream);

            #endregion
        }
    }

    public static class LogRequestMiddlewareExtension
    {
        public static IApplicationBuilder UseLogRequest(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LogRequestMiddleware>();
        }
    }
}
