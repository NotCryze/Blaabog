using Microsoft.Extensions.Caching.Memory;
using SBO.BlaaBog.Domain.Entities;
using SBO.BlaaBog.Services.Services;

namespace SBO.BlaaBog.Web.Middlewares
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IMemoryCache _cache;
        private readonly TeacherService _teacherService;

        public AuthMiddleware(RequestDelegate next, IMemoryCache cache)
        {
            _next = next;
            _cache = cache;
            _teacherService = new TeacherService();
        }

        public async Task Invoke(HttpContext httpContext)
        {
            httpContext.Items["User"] = _cache.Get(httpContext.Session.Id);


            PathString path = httpContext.Request.Path;

            if (path.HasValue)
            {
                string pathLower = path.Value.ToLower();

                // Method 1
                if (pathLower.StartsWith("/Teachers".ToLower()))
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
}