using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;
using WebApi.Exceptions;
using Newtonsoft.Json;

namespace WebApi.Middleware
{
    /// <summary>
    /// Error handling middleware.
    /// </summary>
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;


        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorHandlingMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next.</param>
        /// <param name="logger"></param>
        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            this.next = next;
            _logger = logger;

        }

        /// <summary>
        /// Invoke.
        /// </summary>
        /// <param name="context">The context.</param>
        public async Task Invoke(HttpContext context /* other dependencies */)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, _logger);
            }
        }

        /// <summary>
        /// Handles exception async.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="logger">The exception.</param>
        private static Task HandleExceptionAsync(HttpContext context, Exception exception, ILogger<ErrorHandlingMiddleware> logger)
        {
            var code = HttpStatusCode.InternalServerError; // 500 if unexpected

            if (exception is ConflictException) code = HttpStatusCode.Conflict;
            else if (exception is NotFoundException) code = HttpStatusCode.NotFound;
                       
            var textError = exception.Message;
            if (!string.IsNullOrEmpty(exception.InnerException?.Message))
            {
                textError += ". " + exception.InnerException.Message;
            }

            logger.LogError(textError);

            var errorCode = exception is CustomException ? ((CustomException)exception).CodeError : "";
            var result = JsonConvert.SerializeObject(new { ErrorText = textError, ErrorCode = errorCode });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}
