namespace ShopFlowAPI.Models.DTOs;

public class ApiResponse
{
    public int StatusCode { get; set; }
    public required string Message { get; set; }
    public object? Data { get; set; }

    public static ApiResponse Ok( string message = "Success",  object? data = null)
    {
        return new ApiResponse
        {
            StatusCode = 200,
            Message = message,
            Data = data
        };
    }

    public static ApiResponse Fail( int statusCode = 400, string message = "Something went wrong", object? data = null)
    {
        return new ApiResponse
        {
            StatusCode = statusCode,
            Message = message,
            Data = data
        };
    }

    public static ApiResponse NotFound( string message = "Resource not found")
    {
        return new ApiResponse
        {
            StatusCode = 404,
            Message = message,
            Data = null
        };
    }
}