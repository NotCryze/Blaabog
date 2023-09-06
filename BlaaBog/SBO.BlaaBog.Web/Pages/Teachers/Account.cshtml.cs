using BCrypt.Net;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SBO.BlaaBog.Domain.Entities;
using SBO.BlaaBog.Services.Services;
using SBO.BlaaBog.Web.DTO;
using SBO.BlaaBog.Web.Utils;
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

        #region Update Account

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
                HttpContext.Session.AddToastNotification(new ToastNotification { Message = "Account has been updated", Status = ToastColor.Success });
            }
            catch (Exception exception)
            {
                await Console.Out.WriteLineAsync(exception.Message);
                HttpContext.Session.AddToastNotification(new ToastNotification { Message = "Something went wrong", Status = ToastColor.Danger });
            }

            return await OnGetAsync();
        }

        #endregion

        #region Change Password

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
                    HttpContext.Session.AddToastNotification(new ToastNotification { Message = "Password has been changed!", Status = ToastColor.Success });
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
                HttpContext.Session.AddToastNotification(new ToastNotification { Message = "Something went wrong", Status = ToastColor.Danger });
            }

            return await OnGetAsync();
        }

        #endregion
    }
}
