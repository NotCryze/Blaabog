using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using SBO.BlaaBog.Domain.Entities;
using SBO.BlaaBog.Services.Services;
using SBO.BlaaBog.Web.DTO;
using System.Runtime.CompilerServices;
using BC = BCrypt.Net.BCrypt;

namespace SBO.BlaaBog.Web.Pages.Teachers
{
    public class LoginModel : PageModel
    {
        private readonly TeacherService _teacherService;
        private readonly IMemoryCache _cache;
        public LoginModel(IMemoryCache cache)
        {
            _teacherService = new TeacherService();
            _cache = cache;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            return Page();
        }

        [BindProperty]
        public LoginDTO Login { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Teacher? teacher = await _teacherService.GetTeacherByEmailAsync(Login.Email);

            if (teacher != null)
            {
                bool validatePassword = BC.EnhancedVerify(Login.Password, teacher.Password);
                if (validatePassword)
                {

                    HttpContext.Session.SetInt32("Id", Convert.ToInt32(teacher.Id));
                    HttpContext.Session.SetString("Name", teacher.Name);
                    _cache.Set(HttpContext.Session.Id, teacher);
                    return Redirect("/Teachers/Index");
                }
                else
                {
                    await Console.Out.WriteLineAsync("Wrong Password");
                }
            }
            else
            {
                await Console.Out.WriteLineAsync("Student is null");
            }




            return Page();
        }
    }
}
