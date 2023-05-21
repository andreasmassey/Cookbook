using System;
using System.Net;
using System.Text;
using Cookbook.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
namespace Gateway.Extensions
{
    public static class SwaggerServiceExtensions
    {        
        public static IApplicationBuilder UseSwaggerAuthorization(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SwaggerAuthorizedMiddleware>();
        }
    }

    public class SwaggerAuthorizedMiddleware
    {
        private readonly RequestDelegate _next;
        public SwaggerAuthorizedMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async System.Threading.Tasks.Task InvokeAsync(HttpContext context, IConfiguration configuration)
        {
            if (context.Request.Path.StartsWithSegments("/swagger"))
            {
                string authHeader = context.Request.Headers["Authorization"];

                if (authHeader != null && authHeader.StartsWith("Basic"))
                {
                    // Get the encoded username and password
                    var encodedUsernamePassword = authHeader.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries)[1]?.Trim();
                    // Decode from Base64 to string
                    var decodedUsernamePassword = Encoding.UTF8.GetString(Convert.FromBase64String(encodedUsernamePassword));
                    // Split username and password
                    var username = decodedUsernamePassword.Split(':', 2)[0]; var password = decodedUsernamePassword.Split(':', 2)[1];
                    // Check if login is correct
                    if (IsAuthorized(username, password, configuration))
                    {
                        await _next.Invoke(context);
                        return;
                    }
                }

                //Return authentication type (causes browser to show login dialog)
                context.Response.Headers["WWW-Authenticate"] = "Basic";

                //Return unauthorized(401)
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            }
            else
            {
                await _next.Invoke(context);
            }
        }

        public bool IsAuthorized(string username, string secret, IConfiguration configuration)
        {
            string swaggerUser = configuration["AppSettings:SwaggerUser"];
            string swaggerPassword = configuration["AppSettings:SwaggerPassword"];
            string passwordHash = configuration["AppSettings:SwaggerPasswordHash"];

            var saltByte = Convert.FromBase64String(passwordHash);
            var password = EncryptPassword.HashPassword(secret, saltByte);

            // Check that username and password are correct
            return username.Equals(swaggerUser, StringComparison.InvariantCultureIgnoreCase) && password.Equals(swaggerPassword);
        }
    }
}