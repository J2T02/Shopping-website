using Store.Exceptions;
using System.Net;
using System.Text.Json;

namespace Store.Middleware
{
    public class GlobalExceptionHandling
    {
        private readonly RequestDelegate _next;
        public GlobalExceptionHandling(RequestDelegate next) => _next = next;
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }
        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            string message = "Something went wrong!";
            switch (ex)
            {
                case ValidationException:
                    statusCode = HttpStatusCode.BadRequest;
                    message = ex.Message;
                    break;
                case NotFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    message = ex.Message;
                    break;
                default:
                    message = ex.Message;
                    break;
            }
            context.Response.StatusCode = (int)statusCode;
            var response = new { Message = message };
            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
