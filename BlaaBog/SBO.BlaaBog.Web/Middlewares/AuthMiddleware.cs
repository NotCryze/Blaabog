using Microsoft.AspNetCore.Mvc;
using SBO.BlaaBog.Domain.Entities;
using System.Reflection.Metadata.Ecma335;

namespace SBO.BlaaBog.Web.Middlewares
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            //httpContext.Items["User"] = new Teacher(1, "Name", "Email", "Password", false);
            //httpContext.Items["User"] = new Student(1, "Name", "Image", "Description", "Email", "Speciality", 1, new DateOnly(), "Password");
            //httpContext.Items["User"] = null;

            PathString path = httpContext.Request.Path;

            if (httpContext.Items["User"] is Teacher)
            {
                await Console.Out.WriteLineAsync("Hello lol");
            }


            if (path.HasValue)
            {
                string pathLower = path.Value.ToLower();

                // Method 1

                if (pathLower.StartsWith("/Teacher".ToLower()))
                {
                    if (httpContext.Items["User"] is Teacher)
                    {
                    }
                    else if (pathLower.StartsWith("/Teacher/Login".ToLower()) || pathLower.StartsWith("/Teacher/Register".ToLower()))
                    {
                    }
                    else if (pathLower.StartsWith("/Error".ToLower()))
                    {
                    }
                    else
                    {
                        httpContext.Response.Redirect("/Teacher/Login");
                        return;
                    }
                }

                // Method 2
                else if (pathLower.StartsWith("/".ToLower()))
                {
                    if (httpContext.Items["User"] == null)
                    {
                        if (!pathLower.StartsWith("/Login".ToLower()) && !pathLower.StartsWith("/Register".ToLower()) && !pathLower.StartsWith("/Error".ToLower()))
                        {
                            httpContext.Response.Redirect("/Login");
                            return;
                        }
                    }
                }
            }

            await _next(httpContext);
        }
    }

    public static class AuthMiddlewareExtensions
    {
        public static IApplicationBuilder UseAuthMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthMiddleware>();
        }
    }
}
