using backend.DTO;
using backend.Exception;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace backend.Middleware
{
    public class ExceptionHandlerMiddleware : ExceptionFilterAttribute
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);  // Call the next middleware in the pipeline
            }
            catch (NotFoundException ex)
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                context.Response.ContentType = "application/json";

                var response = new StandardResponse(404, "Error", ex.Message);
                await context.Response.WriteAsJsonAsync(response);
            }
            // You can add more exception handlers for different types of exceptions if necessary
        }
    }
}
