using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SBO.BlaaBog.Domain.Connections;
using SBO.BlaaBog.Domain.Entities;
using SBO.BlaaBog.Services.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SBO.BlaaBog.Web.Pages
{
    public class IndexModel : PageModel
    {
        public async Task<IActionResult> OnGetAsync()
        {
            return Page();
        }
    }
}
