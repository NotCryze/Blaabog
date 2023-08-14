using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using SBO.BlaaBog.Domain.Entities;
using SBO.BlaaBog.Services.Services;
using SBO.BlaaBog.Web.DTO;
using SBO.BlaaBog.Web.Utils;
using BC = BCrypt.Net.BCrypt;

namespace SBO.BlaaBog.Web.Pages
{
    public class RegisterModel : PageModel
    {
        private StudentService _studentService;
        private ClassService _classService;

        private readonly IMemoryCache _cache;

        public RegisterModel(IMemoryCache cache)
        {
            _studentService = new StudentService();
            _classService = new ClassService();

            _cache = cache;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            await Console.Out.WriteLineAsync("Hello");

            return Page();
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

                Student? studentFound = await _studentService.GetStudentByEmailAsync(Register.Email);

                if (studentFound == null)
                {
                    Class? classFound = await _classService.GetClassByTokenAsync(Register.Token);

                    if (classFound != null && classFound.Token == Register.Token)
                    {
                        string passwordHash = BC.EnhancedHashPassword(Register.Password);

                        Student student = new Student(0, Register.Name, null, null, Register.Email, null, 0, null, passwordHash);

                        bool success = await _studentService.CreateStudentAsync(student, Convert.ToInt32(classFound.Id));

                        if (success)
                        {
                            Student createdStudent = await _studentService.GetStudentByEmailAsync(Register.Email);

                            HttpContext.Session.Clear();

                            HttpContext.Session.SetInt32("Id", Convert.ToInt32(createdStudent.Id));
                            HttpContext.Session.SetString("Name", createdStudent.Name);
                            _cache.Set(HttpContext.Session.Id, createdStudent);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Register.Token", "Token does not exist");
                        return Page();
                    }
                }
                else
                {
                    ModelState.AddModelError("Register.Email", "Email already exists");
                }
            }
            catch (Exception exception)
            {
                await Console.Out.WriteLineAsync(exception.Message);
            }

            return Page();
        }
    }
}
