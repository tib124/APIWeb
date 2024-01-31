using APIWeb.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Authentication
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _config;

        public ApiKeyMiddleware(RequestDelegate next, IConfiguration config)
        {
            _next = next;
            _config = config;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var providedApiKey = context.Request.Headers[AuthConfig.ApiKeyHeader].FirstOrDefault();
            var isValid = IsValidApiKey(providedApiKey);

            if (!isValid)
            {
                await GenerateResponse(context, 401, "Invalid Authentication");
                return;
            }

            await _next(context);
        }

        private async Task GenerateResponse(HttpContext context, int statusCode, string message)
        {
            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsync(message);
        }

        private bool IsValidApiKey(string providedApiKey)
        {
            if (string.IsNullOrEmpty(providedApiKey))
            {
                return false;
            }

            var validApiKey = _config.GetValue<string>(AuthConfig.AuthSection);

            return string.Equals(validApiKey, providedApiKey, StringComparison.Ordinal);
        }
    }
}
