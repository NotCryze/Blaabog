using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using SBO.BlaaBog.Domain.Entities;
using SBO.BlaaBog.Services.Services;
using SBO.BlaaBog.Web.DTO;
using BC = BCrypt.Net.BCrypt;

namespace SBO.BlaaBog.Web.Pages
{
    public class LoginModel : PageModel
    {
        private readonly StudentService _studentService;
        private readonly IMemoryCache _cache;
        public LoginModel(IMemoryCache cache)
        {
            _studentService = new StudentService();
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
            try
            {
                if (!ModelState.IsValid)
                {
                    return await OnGetAsync();
                }

                Student? student = await _studentService.GetStudentByEmailAsync(Login.Email);

                if (student != null)
                {
                    bool validatePassword = BC.EnhancedVerify(Login.Password, student.Password);
                    if (validatePassword)
                    {
                        HttpContext.Session.SetInt32("Id", Convert.ToInt32(student.Id));
                        HttpContext.Session.SetString("Name", student.Name);
                        _cache.Set(HttpContext.Session.Id, student);
                        return RedirectToPage("/Index");
                    }
                    else
                    {
                        await Console.Out.WriteLineAsync("Wrong password");
                        ModelState.AddModelError("Login", "Wrong email or password");
                    }
                }
                else
                {
                    await Console.Out.WriteLineAsync("Student is null");
                    ModelState.AddModelError("Login", "Wrong email or password");
                }
            }
            catch (Exception exception)
            {
                await Console.Out.WriteLineAsync(exception.Message);
                ModelState.AddModelError("Login", "Something went wrong");
            }

            return await OnGetAsync();
        }
    }
}
