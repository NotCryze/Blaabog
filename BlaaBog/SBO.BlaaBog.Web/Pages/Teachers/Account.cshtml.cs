using BCrypt.Net;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SBO.BlaaBog.Domain.Entities;
using SBO.BlaaBog.Services.Services;
using SBO.BlaaBog.Web.DTO;
using System.ComponentModel.DataAnnotations;

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

        [BindProperty]
        public ChangePasswordDTO ChangePassword { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                Teacher teacher = await _teacherService.GetTeacherAsync(Convert.ToInt32(HttpContext.Session.GetInt32("Id")));

                Account = new TeacherAccountDTO { Name = teacher.Name, Email = teacher.Email };
            }
            catch (Exception exception)
            {
                await Console.Out.WriteLineAsync(exception.Message);
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (ModelState.GetFieldValidationState("Account.Name") == ModelValidationState.Invalid
                || ModelState.GetFieldValidationState("Account.Email") == ModelValidationState.Invalid)
                {
                    return await OnGetAsync();
                }

                Teacher teacher = await _teacherService.GetTeacherAsync(Convert.ToInt32(HttpContext.Session.GetInt32("Id")));

                Teacher newTeacher = new Teacher(teacher.Id, Account.Name, Account.Email, teacher.Password, teacher.Admin);

                await _teacherService.UpdateTeacherAsync(newTeacher);
            }
            catch (Exception exception)
            {
                await Console.Out.WriteLineAsync(exception.Message);
            }

            return await OnGetAsync();
        }

        public async Task<IActionResult> OnPostChangePasswordAsync()
        {
            try
            {
                Teacher teacher = await _teacherService.GetTeacherAsync(Convert.ToInt32(HttpContext.Session.GetInt32("Id")));

                if (!BCrypt.Net.BCrypt.EnhancedVerify(ChangePassword.Old, teacher.Password))
                {
                    ModelState.AddModelError("ChangePassword.Old", "Incorrect Password");
                }

                if (ModelState.GetFieldValidationState("ChangePassword.Old") == ModelValidationState.Invalid
                    || ModelState.GetFieldValidationState("ChangePassword.New") == ModelValidationState.Invalid
                    || ModelState.GetFieldValidationState("ChangePassword.Confirm") == ModelValidationState.Invalid
                    )
                {
                    return await OnGetAsync();
                }

                string passwordHash = BCrypt.Net.BCrypt.EnhancedHashPassword(ChangePassword.New);

                Teacher newTeacher = new Teacher(teacher.Id, teacher.Name, teacher.Email, passwordHash, teacher.Admin);

                bool success = await _teacherService.UpdateTeacherAsync(newTeacher);

                if (success)
                {
                    return RedirectToPage("/Teachers/Account");
                }
                else
                {
                    ModelState.AddModelError("ChangePassword", "Something went wrong");
                }
            }
            catch (Exception exception)
            {
                await Console.Out.WriteLineAsync(exception.Message);
            }

            return await OnGetAsync();
        }
    }
}
