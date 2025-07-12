using FoodApplication.Domain.Data.Enums;
using FoodApplication.Presentation.ViewModel;
using System.Net;

namespace FoodApplication.Presentation.Middlewares
{
    public class GlobalErrorHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<GlobalErrorHandlingMiddleware> _logger;

        public GlobalErrorHandlingMiddleware(ILogger<GlobalErrorHandlingMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            
            if (next == null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var errorResponse = CreateErrorResponse(ex);

            LogError(ex, context);

            await WriteErrorResponseAsync(context, errorResponse);
        }

        private static ResponseViewModel<object> CreateErrorResponse(Exception ex)
        {
            var errorMessage = ex.InnerException?.Message ?? ex.Message;

            var exceptionType = ex.GetType();

            ErrorCode errorCode;
            if (exceptionType == typeof(ArgumentException) ||
                exceptionType == typeof(ArgumentNullException) ||
                exceptionType == typeof(InvalidOperationException))
            {
                errorCode = ErrorCode.BadRequest;
            }
            else if (exceptionType == typeof(UnauthorizedAccessException))
            {
                errorCode = ErrorCode.Unauthorized;
            }
            else if (exceptionType == typeof(KeyNotFoundException))
            {
                errorCode = ErrorCode.NotFound;
            }
            else
            {
                errorCode = ErrorCode.BadRequest;
            }

            return ResponseViewModel<object>.Failure(errorCode, errorMessage);
        }

        private void LogError(Exception ex, HttpContext context)
        {
            var errorMessage = ex.InnerException?.Message ?? ex.Message;

            _logger.LogError(ex,
                "Unhandled exception occurred. Method: {Method}, Path: {Path}, Error: {Error}",
                context.Request.Method,
                context.Request.Path,
                errorMessage);
        }

        private static async Task WriteErrorResponseAsync(HttpContext context, ResponseViewModel<object> errorResponse)
        {
            context.Response.StatusCode = GetHttpStatusCode(errorResponse.ErrorCode);
            context.Response.ContentType = "application/json";

            if (!context.Response.HasStarted)
            {
                await context.Response.WriteAsJsonAsync(errorResponse);
            }
        }

        private static int GetHttpStatusCode(ErrorCode errorCode)
        {
            return errorCode switch
            {
                ErrorCode.Ok => (int)HttpStatusCode.OK,
                ErrorCode.BadRequest => (int)HttpStatusCode.BadRequest,
                ErrorCode.Unauthorized => (int)HttpStatusCode.Unauthorized,
                ErrorCode.Forbidden => (int)HttpStatusCode.Forbidden,
                ErrorCode.NotFound => (int)HttpStatusCode.NotFound,
                ErrorCode.None => (int)HttpStatusCode.InternalServerError,
                _ => (int)HttpStatusCode.InternalServerError
            };
        }
    }
}
