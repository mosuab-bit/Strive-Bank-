using System.Net;
using System.Text.Json;

namespace BankSystem.API.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context); // مرر الطلب للخطوة اللي بعدها
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex); // لو صار خطأ، تعامل معه
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var code = ex switch
            {
                
                KeyNotFoundException => HttpStatusCode.NotFound,              // 404
                UnauthorizedAccessException => HttpStatusCode.Forbidden,     // 403
                ArgumentException => HttpStatusCode.BadRequest,              // 400
                InvalidOperationException => HttpStatusCode.BadRequest,
                _ => HttpStatusCode.InternalServerError                      // 500
            };

            var result = JsonSerializer.Serialize(new
            {
                status = (int)code,
                message = ex.Message
            });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            return context.Response.WriteAsync(result);
        }
    }
}
