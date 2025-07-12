using FoodApplication.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace FoodApplication.Presentation.Middlewares
{
    public class TransactionMiddleware : IMiddleware
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<TransactionMiddleware> _logger;
        private static readonly HashSet<string> ReadOnlyMethods = new()
        {
            HttpMethods.Get,
            HttpMethods.Head,
            HttpMethods.Options
        };

        public TransactionMiddleware(ApplicationDbContext context, ILogger<TransactionMiddleware> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task InvokeAsync(HttpContext httpContext, RequestDelegate next)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException(nameof(httpContext));
            }
            
            if (next == null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            if (ShouldSkipTransaction(httpContext))
            {
                await next(httpContext);
                return;
            }

            await ExecuteWithTransactionAsync(httpContext, next);
        }

        private static bool ShouldSkipTransaction(HttpContext httpContext)
        {
            return ReadOnlyMethods.Contains(httpContext.Request.Method);
        }

        private async Task ExecuteWithTransactionAsync(HttpContext httpContext, RequestDelegate next)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            _logger.LogDebug("Database transaction started for {Method} {Path}", httpContext.Request.Method, httpContext.Request.Path);

            try
            {
                await next(httpContext);

                await transaction.CommitAsync();
                _logger.LogDebug("Database transaction committed successfully");
            }
            catch (Exception ex)
            {
                await RollbackTransactionAsync(transaction, ex);
                throw; // Re-throw for global error handler
            }
        }

        private async Task RollbackTransactionAsync(IDbContextTransaction transaction, Exception originalException)
        {
            try
            {
                await transaction.RollbackAsync();
                _logger.LogWarning("Database transaction rolled back due to exception: {Error}", originalException.InnerException?.Message ?? originalException.Message);
            }
            catch (Exception rollbackEx)
            {
                _logger.LogError(rollbackEx, "Failed to rollback transaction. Original exception: {OriginalError}", originalException.Message);
            }
        }
    }
}