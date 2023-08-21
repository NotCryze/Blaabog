using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;

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
