using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;
using FluentValidation; // إذا تستخدم FluentValidation

namespace WebApi.Middlewares
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
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
            catch (Exception ex)
            {
                // أي استثناء يحدث هنا
                await HandleExceptionAsync(context, ex, _logger);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception ex, ILogger logger)
        {
            // 🔹 Default status
            int statusCode = (int)HttpStatusCode.InternalServerError;
            string title = "Internal Server Error";
            string detail = ex.Message;

            // 🔹 نوع الخطأ - FluentValidation
            if (ex is ValidationException validationEx)
            {
                statusCode = (int)HttpStatusCode.BadRequest;
                title = "Validation Failed";
                detail = string.Join("; ", validationEx.Errors.Select(e => e.ErrorMessage));
            }
            // 🔹 نوع الخطأ - NotFoundException (يمكنك تعريفه بنفسك)
            else if (ex is KeyNotFoundException)
            {
                statusCode = (int)HttpStatusCode.NotFound;
                title = "Resource Not Found";
            }
            // 🔹 نوع الخطأ - Unauthorized
            else if (ex is UnauthorizedAccessException)
            {
                statusCode = (int)HttpStatusCode.Unauthorized;
                title = "Unauthorized";
            }

            // تسجيل الخطأ
            logger.LogError(ex, "Unhandled Exception");

            // إعداد Response
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/problem+json";

            var problem = new ProblemDetails
            {
                Status = statusCode,
                Title = title,
                Detail = detail,
                Instance = context.Request.Path
            };

            await context.Response.WriteAsJsonAsync(problem);
        }
    }
}
