using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using SBO.BlaaBog.Domain.Entities;
using SBO.BlaaBog.Services.Services;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SBO.BlaaBog.Web.DTO;
using BC = BCrypt.Net.BCrypt;

namespace SBO.BlaaBog.Web.Pages.Teachers
{
    public class RegisterModel : PageModel
    {
        private readonly TeacherService _teacherService;
        private readonly TeacherTokenService _teacherTokenService;
        private readonly IMemoryCache _cache;

        public RegisterModel(IMemoryCache cache)
        {
            _teacherService = new TeacherService();
            _teacherTokenService = new TeacherTokenService();
            _cache = cache;
        }

        public void OnGet()
        {
        }

        [BindProperty]
        public RegisterDTO Register { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            await Console.Out.WriteLineAsync(Register.Token);

            try
            {
                TeacherToken? tokenFound = await _teacherTokenService.GetTeacherTokenByTokenAsync(Register.Token);
                Teacher? teacherFound = await _teacherService.GetTeacherByEmailAsync(Register.Email);

                if (tokenFound == null)
                {
                    ModelState.AddModelError("Register.Token", "Token does not exist");
                }

                if (teacherFound != null)
                {
                    ModelState.AddModelError("Register.Email", "Email already in use");
                }

                if (!ModelState.IsValid)
                {
                    return Page();
                }

                if (teacherFound == null && tokenFound != null && tokenFound.Token == Register.Token)
                {
                    string passwordHash = BC.EnhancedHashPassword(Register.Password);

                    Teacher teacher = new Teacher(0, Register.Name, Register.Email, passwordHash, false);

                    bool success = await _teacherService.CreateTeacherAsync(teacher);

                    if (success)
                    {
                        Teacher createdTeacher = await _teacherService.GetTeacherByEmailAsync(teacher.Email);

                        bool tokenUpdated = await _teacherTokenService.UseTeacherTokenAsync(Convert.ToInt32(tokenFound.Id), Convert.ToInt32(createdTeacher.Id));

                        HttpContext.Session.Clear();

                        HttpContext.Session.SetInt32("Id", Convert.ToInt32(createdTeacher.Id));
                        HttpContext.Session.SetString("Name", createdTeacher.Name);
                        _cache.Set(HttpContext.Session.Id, createdTeacher);

                        return RedirectToPage("/Teachers/Index");
                    }
                    else
                    {
                        ModelState.AddModelError("Register", "Something went wrong");
                    }
                }
            }
            catch (Exception exception)
            {
                await Console.Out.WriteLineAsync(exception.Message);
                ModelState.AddModelError("Register", "Something went wrong");
            }

            return Page();
        }
    }
}
