using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using SBO.BlaaBog.Domain.Entities;
using SBO.BlaaBog.Services.Services;
using SBO.BlaaBog.Web.DTO;

namespace SBO.BlaaBog.Web.Pages.Teachers
{
    public class RegisterModel : PageModel
    {
        private readonly TeacherService _teacherService;

        public RegisterModel()
        {
            _teacherService = new TeacherService();
        }

        public void OnGet()
        {
        }

        [BindProperty]
        public RegisterDTO Register { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Page();
                }

                Teacher? teacherFound = await _teacherService.GetTeacherByEmailAsync(Register.Email);

                if (teacherFound == null)
                {

                }
            }
            catch (Exception exception)
            {
                await Console.Out.WriteLineAsync(exception.Message);
            }
        }
    }
}
