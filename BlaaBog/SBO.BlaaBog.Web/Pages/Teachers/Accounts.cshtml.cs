using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Win32;
using SBO.BlaaBog.Domain.Entities;
using SBO.BlaaBog.Services.Services;
using SBO.BlaaBog.Web.DTO;
using BC = BCrypt.Net.BCrypt;

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
            try
            {
                Teachers = await _teacherService.GetTeachersAsync();
            }
            catch (Exception exception)
            {
                await Console.Out.WriteLineAsync(exception.Message);
                throw;
            }

            return Page();
        }


        [BindProperty]
        public RegisterDTO CreateAccount { get; set; }

        public async Task<IActionResult> OnPostCreate()
        {
            try
            {
                Teacher? teacherFound = await _teacherService.GetTeacherByEmailAsync(CreateAccount.Email);

                if (teacherFound != null)
                {
                    ModelState.AddModelError("CreateAccount.Email", "Email already in use");
                }

                if (
                    ModelState.GetFieldValidationState("CreateAccount.Name") == ModelValidationState.Invalid ||
                    ModelState.GetFieldValidationState("CreateAccount.Email") == ModelValidationState.Invalid ||
                    ModelState.GetFieldValidationState("CreateAccount.Password") == ModelValidationState.Invalid ||
                    ModelState.GetFieldValidationState("CreateAccount.ConfirmPassword") == ModelValidationState.Invalid
                    )
                {
                    return await OnGetAsync();
                }

                string passwordHash = BC.EnhancedHashPassword(CreateAccount.Password);
                Teacher teacher = new Teacher(0, CreateAccount.Name, CreateAccount.Email, passwordHash, false);

                bool success = await _teacherService.CreateTeacherAsync(teacher);

                if (success)
                {
                    Teacher createdTeacher = await _teacherService.GetTeacherByEmailAsync(teacher.Email);

                    return RedirectToPage("/Teachers/Accounts");
                }
                else
                {
                    ModelState.AddModelError("Register", "Something went wrong");
                }
            }
            catch (Exception exception)
            {
                await Console.Out.WriteLineAsync(exception.Message);
            }

            return await OnGetAsync();
        }


        [BindProperty]
        public EditTeacherDTO EditAccount { get; set; }
        
        public int EditAccountId { get; set; }

        public async Task<IActionResult> OnPostEditAsync(int id)
        {
            try
            {
                EditAccountId = id;

                Teacher? teacherFound = await _teacherService.GetTeacherByEmailAsync(CreateAccount.Email);

                if (teacherFound != null)
                {
                    ModelState.AddModelError("EditAccount.Email", "Email already in use");
                }

                if (
                    ModelState.GetFieldValidationState("EditAccount.Name") == ModelValidationState.Invalid ||
                    ModelState.GetFieldValidationState("EditAccount.Email") == ModelValidationState.Invalid ||
                    ModelState.GetFieldValidationState("EditAccount.Admin") == ModelValidationState.Invalid
                    )
                {
                    return await OnGetAsync();
                }

                Teacher teacher = await _teacherService.GetTeacherAsync(id);
                Teacher newTeacher = new Teacher(id, EditAccount.Name, EditAccount.Email, teacher.Password, EditAccount.Admin);

                await Console.Out.WriteLineAsync(EditAccount.Admin.ToString());

                bool status = await _teacherService.UpdateTeacherAsync(newTeacher);

                if (status)
                {
                    return RedirectToPage("/Teachers/Accounts");
                }
                else
                {
                    ModelState.AddModelError("Edit", "Something went wrong");

                    return await OnGetAsync();
                }
            }
            catch (Exception exception)
            {
                await Console.Out.WriteLineAsync(exception.Message);
            }

            return Redirect("/Teachers/Accounts");
        }


        [BindProperty]
        public int DeleteAccount { get; set; }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            try
            {
                bool status = await _teacherService.DeleteTeacherAsync(id);
            }
            catch (Exception exception)
            {
                await Console.Out.WriteLineAsync(exception.Message);
            }

            return Redirect("/Teachers/Accounts");
        }
    }
}
