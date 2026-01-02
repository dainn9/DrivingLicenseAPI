using System.Net;
using System.Text.Json;
using DrivingLicense.Application.Common.ApiResponses;
using DrivingLicense.Application.Common.Exceptions;
using Humanizer;

namespace DrivingLicense.API.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Unhandled exception occurred while processing request: {Method} {Path}. Exception: {ExceptionType}",
                    context.Request.Method,
                    context.Request.Path,
                    e.GetType().Name);
                await HandleExceptionAsync(context, e);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            HttpStatusCode httpStatusCode;
            string message = exception.Message;

            switch (exception)
            {
                //404
                case NotFoundException:
                    httpStatusCode = HttpStatusCode.NotFound;
                    break;
                //401
                case UnauthorizedAccessException:
                    httpStatusCode = HttpStatusCode.Unauthorized;
                    break;
                //400
                case BadRequestException:
                    httpStatusCode = HttpStatusCode.BadRequest;
                    break;
                //403
                case ForbiddenException:
                    httpStatusCode = HttpStatusCode.Forbidden;
                    break;
                //409
                case ConflictException:
                    httpStatusCode = HttpStatusCode.Conflict;
                    break;
                case InvalidOperationException:
                    httpStatusCode = HttpStatusCode.InternalServerError;
                    break;
                //500
                default:
                    httpStatusCode = HttpStatusCode.InternalServerError;
                    message = "An unexpected error occurred.";
                    break;
            }

            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)httpStatusCode;

            var response = ApiResponse<string>.FailureResponse(message);

            await httpContext.Response.WriteAsJsonAsync(response);
        }
    }
}