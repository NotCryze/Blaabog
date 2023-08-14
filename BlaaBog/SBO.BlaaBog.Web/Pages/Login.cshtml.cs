using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SBO.BlaaBog.Web.Pages
{
    public class LoginModel : PageModel
    {
        public void OnGet()
        {
            HttpContext.Session.SetString("LoggedIn", "1");
            Console.WriteLine(HttpContext.Session.GetString("LoggedIn"));
        }
    }
}
