using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using SBO.BlaaBog.Domain.Entities;
using System.Reflection.Metadata.Ecma335;

namespace SBO.BlaaBog.Web.Middlewares
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IMemoryCache _cache;

        public AuthMiddleware(RequestDelegate next, IMemoryCache cache)
        {
            _next = next;
            _cache = cache;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            //httpContext.Items["User"] = new Teacher(1, "Name", "Email", "Password", false);
            //httpContext.Items["User"] = new Student(1, "Name", "Image", "Description", "Email", "Speciality", 1, new DateOnly(), "Password");

            httpContext.Items["User"] = _cache.Get(httpContext.Session.Id);

            await Console.Out.WriteLineAsync(httpContext.Session.Id);
            await Console.Out.WriteLineAsync(httpContext.Session.GetInt32("LoggedIn").ToString());

            if (_cache.Get(httpContext.Session.Id) != null)
            {
                await Console.Out.WriteLineAsync(_cache.Get(httpContext.Session.Id).ToString());
            }

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
                    else if (pathLower.StartsWith("/Teachers/Login".ToLower()) || pathLower.StartsWith("/Teachers/Register".ToLower()))
                    {
                    }
                    else if (pathLower.StartsWith("/Error".ToLower()))
                    {
                    }
                    else
                    {
                        httpContext.Response.Redirect("/Teachers/Login");
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

    public static class SessionExtensions
    {
        public static void SetObject(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetObject<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
