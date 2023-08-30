using FluentValidation;
using MuzShop.ExceptionMiddleware.CustomExceptions;
using MuzShop.ExceptionMiddleware.Models;
using System.Net;

namespace MuzShop.ExceptionMiddleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            int code = StatusCodes.Status500InternalServerError;
            string message = "Internal Server Error";


            List<ErrorModel> errors = new();

            switch (exception)
            {
                case ValidationException validationEx:
                    code = StatusCodes.Status400BadRequest;
                    message = "Validation failed!";

                    foreach (var error in validationEx.Errors)
                    {
                        errors.Add(new ErrorModel { PropertyName = error.PropertyName, ErrorMessage = error.ErrorMessage });
                    }

                    break;

                case NotFoundException notFoundEx:
                    code = StatusCodes.Status404NotFound;
                    message = "Not found";
                    break;

                case Exception ex:
                    code = (int)HttpStatusCode.InternalServerError;
                    message = ex.Message;
                    break;
            }

            context.Response.StatusCode = code;
            await context.Response.WriteAsync(new ErrorDetails
            {
                StatusCode = code,
                Message = message,
                Errors = errors
            }.ToString());
        }
    }
}
