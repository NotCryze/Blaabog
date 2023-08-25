using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SBO.BlaaBog.Web.Pages
{
    public class StudentsModel : PageModel
    {
        public void OnGet(int id)
        {
            Console.WriteLine("Test");
        }
    }
}
