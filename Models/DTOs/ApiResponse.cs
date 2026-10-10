namespace ShopFlowAPI.Models.DTOs;

public class ApiResponse
{
    public int StatusCode { get; set; }
    public required string Message { get; set; }
    public object? Data { get; set; }

    public static ApiResponse Ok( string message = "Success", object? data = null)
    {
        return Custom(200, message, data);
    }

    public static ApiResponse Fail( int statusCode = 400, string message = "Something went wrong", object? data = null)
    {
        return Custom(statusCode, message, data);
    }

    public static ApiResponse NotFound(string message = "Resource not found")
    {
        return Custom(404, message);
    }

    public static ApiResponse Custom( int statusCode, string message, object? data = null)
    {
        return new ApiResponse
        {
            StatusCode = statusCode,
            Message = message,
            Data = data
        };
    }
}