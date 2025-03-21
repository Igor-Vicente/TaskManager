﻿using System.Text.Json;

namespace TaskManager.Presentation.Configuration
{
    public class MiddlewareException
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<MiddlewareException> _logger;

        public MiddlewareException(RequestDelegate next, ILogger<MiddlewareException> logger)
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
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            _logger.LogError(ex, ex.Message);

            var errorResponse = new
            {
                sucesso = false,
                erros = new[] { "Ocorreu um erro inesperado, mas já estamos verificando" }
            };

            var jsonResponse = JsonSerializer.Serialize(errorResponse);

            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json";

            return context.Response.WriteAsync(jsonResponse);
        }
    }
}
