using System.Net;
using System.Text.Json;
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
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";
            
            var response = ApiResponse .Fail((int)HttpStatusCode.InternalServerError, ex.Message);

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}