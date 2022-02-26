using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using WebApi.UseCases.Exceptions;

namespace WebApi.WebApi.Utils
{
    /// <summary>
    /// Middleware для преобразования исключений в HttpResponse
    /// </summary>
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (EntityNotFoundException e)
            {
                await HandleException(httpContext, HttpStatusCode.NotFound, e);
            } 
            catch (ValidationException e)
            {
                await HandleException(httpContext, HttpStatusCode.BadRequest, e);
            } 
            // сюда добавляем исключения, которые хотим преобразовать в HttpResponse
            // catch на Exception оставляем последним, чтобы преобразовать неучтенные ex в дефотный HttpResponse
            catch (Exception e)
            {
                await HandleException(httpContext, HttpStatusCode.InternalServerError, e);
            } 
        }

        private static async Task HandleException(HttpContext httpContext, HttpStatusCode code, Exception exception)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int) code;
            await httpContext.Response.WriteAsync(exception.Message);
        }
    }
    
    /// <summary>
    /// Добавляет кастомный преобразователь исключений в HttpResponse
    /// </summary>
    public static class ExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionHandlerMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}