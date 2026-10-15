namespace ShopFlowAPI.Exceptions;

/// <summary>
/// Business-rule failure that should reach the client with a specific HTTP status
/// (handled by ExceptionMiddleware).
/// </summary>
public class AppException : Exception
{
    public int StatusCode { get; }

    public AppException(int statusCode, string message) : base(message)
    {
        StatusCode = statusCode;
    }
}

public sealed class NotFoundException(string message) : AppException(404, message);

public sealed class BadRequestException(string message) : AppException(400, message);
