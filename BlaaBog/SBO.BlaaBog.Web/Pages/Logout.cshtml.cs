using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using SBO.BlaaBog.Domain.Entities;
using SBO.BlaaBog.Web.Utils;

namespace SBO.BlaaBog.Web.Pages
{
    public class LogoutModel : PageModel
    {
        private readonly IMemoryCache _cache;
        public LogoutModel(IMemoryCache cache)
        {
            _cache = cache;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            _cache.Remove(HttpContext.Session.Id);
            HttpContext.Session.Clear();
            HttpContext.Session.AddToastNotification(new ToastNotification { Message = "You have been logged out!", Status = ToastColor.Success });
            
            if (HttpContext.Request.GetTypedHeaders().Referer.LocalPath.StartsWith("/Teachers"))
            {
                return RedirectToPage("/Teachers/Login");
            }
            else
            {
                return RedirectToPage("/Login");
            }
        }
    }
}
