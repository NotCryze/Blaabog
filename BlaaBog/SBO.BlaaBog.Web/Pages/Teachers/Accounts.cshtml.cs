using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SBO.BlaaBog.Domain.Entities;
using SBO.BlaaBog.Services.Services;
using SBO.BlaaBog.Web.DTO;

namespace SBO.BlaaBog.Web.Pages.Teachers
{
    public class AccountsModel : PageModel
    {
        private readonly TeacherService _teacherService;
        public AccountsModel()
        {
            _teacherService = new TeacherService();
        }

        public List<Teacher> Teachers { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Teachers = await _teacherService.GetTeachersAsync();

            return Page();
        }


        [BindProperty]
        public RegisterDTO CreateAccount { get; set; }

        public async Task<IActionResult> OnPostCreate()
        {
            return Page();
        }


        [BindProperty]
        public RegisterDTO EditAccount { get; set; }
        public async Task<IActionResult> OnPostEditAsync(int id)
        {
            return Page();
        }


        [BindProperty]
        public int DeleteAccount { get; set; }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            bool status = await _teacherService.DeleteTeacherAsync(id);

            return Redirect("/Teachers/Accounts");
        }
    }
}
