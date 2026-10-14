using System.Net;
using System.Text.Json;
using ShopFlowAPI.Exceptions;
using ShopFlowAPI.Models.DTOs;

namespace ShopFlowAPI.Middlewares;

public class ExceptionMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {

            if (context.Response.HasStarted) throw;

            context.Response.Clear();
            var statusCode = ex is AppException appEx
                ? appEx.StatusCode
                : (int)HttpStatusCode.InternalServerError;

            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";
            
            var response = ApiResponse.Fail(statusCode, ex.Message);

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}