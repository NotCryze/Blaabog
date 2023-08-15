using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SBO.BlaaBog.Domain.Entities;
using SBO.BlaaBog.Services.Services;
using SBO.BlaaBog.Web.DTO;

namespace SBO.BlaaBog.Web.Pages.Teachers
{
    public class AccountModel : PageModel
    {
        private readonly TeacherService _teacherService;
        public AccountModel()
        {
            _teacherService = new TeacherService();
        }

        [BindProperty]
        public TeacherAccountDTO Account { get; set; }

        public async Task<IActionResult> OnGet()
        {
            if (HttpContext.Items["User"] is Student)
            {
                return RedirectToPage("/Account");
            }

            Teacher teacher = await _teacherService.GetTeacherAsync(Convert.ToInt32(HttpContext.Session.GetInt32("Id")));

            Account = new TeacherAccountDTO { Name = teacher.Name, Email = teacher.Email };

            return Page();
        }
    }
}
