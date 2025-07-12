using FoodApplication.Domain.Data.Enums;

namespace FoodApplication.Presentation.ViewModel
{
    public class ResponseViewModel<T>
    {
        public T? Data { get; set; }
        public bool IsSuccess { get; set; }
        public ErrorCode ErrorCode { get; set; }
        public string Message { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        // Private constructor to enforce factory pattern
        private ResponseViewModel()
        {
        }

        public static ResponseViewModel<T> Success(T data, string message = "Request completed successfully")
        {
            return new ResponseViewModel<T>
            {
                Data = data,
                IsSuccess = true,
                Message = message,
                ErrorCode = ErrorCode.Ok
            };
        }

        public static ResponseViewModel<T> Success(string message = "Request completed successfully")
        {
            return new ResponseViewModel<T>
            {
                Data = default,
                IsSuccess = true,
                Message = message,
                ErrorCode = ErrorCode.Ok
            };
        }

        public static ResponseViewModel<T> Failure(ErrorCode errorCode, string message = "")
        {
            return new ResponseViewModel<T>
            {
                Data = default,
                IsSuccess = false,
                Message = string.IsNullOrEmpty(message) ? GetDefaultErrorMessage(errorCode) : message,
                ErrorCode = errorCode
            };
        }

        public static ResponseViewModel<T> Failure(string message, ErrorCode errorCode = ErrorCode.BadRequest)
        {
            return new ResponseViewModel<T>
            {
                Data = default,
                IsSuccess = false,
                Message = message,
                ErrorCode = errorCode
            };
        }

        private static string GetDefaultErrorMessage(ErrorCode errorCode)
        {
            return errorCode switch
            {
                ErrorCode.BadRequest => "Bad request",
                ErrorCode.Unauthorized => "Unauthorized access",
                ErrorCode.Forbidden => "Access forbidden",
                ErrorCode.NotFound => "Resource not found",
                ErrorCode.Ok => "Success",
                ErrorCode.None => "An error occurred",
                _ => "An unexpected error occurred"
            };
        }
    }
}
