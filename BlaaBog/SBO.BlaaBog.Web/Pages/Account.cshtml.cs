using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.OpenApi.Extensions;
using SBO.BlaaBog.Domain.Entities;
using SBO.BlaaBog.Services.Services;
using SBO.BlaaBog.Web.DTO;
using SBO.BlaaBog.Web.Extensions.DataAnnotations;
using SBO.BlaaBog.Web.Utils;
using System.ComponentModel.DataAnnotations;
using BC = BCrypt.Net.BCrypt;

namespace SBO.BlaaBog.Web.Pages
{
    public class AccountModel : PageModel
    {
        private readonly StudentService _service;
        private readonly IWebHostEnvironment _environment;
        public AccountModel(IWebHostEnvironment environment)
        {
            _service = new StudentService();
            _environment = environment;
        }

        [BindProperty]
        public StudentAccountDTO Student { get; set; }
        public List<SelectListItem> SpecialitiesList { get; set; } = new List<SelectListItem>();
        public string ImageName { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            if (HttpContext.Items["User"] is Teacher)
            {
                return RedirectToPage("/Teachers/Register");
            }

            Student? student = await _service.GetStudentAsync(Convert.ToInt32(HttpContext.Session.GetInt32("Id")));

            Student = new StudentAccountDTO
            {
                Name = student.Name,
                Description = student.Description,
                Email = student.Email,
                Speciality = student.Speciality,
                EndDate = student.EndDate
            };

            foreach (Specialities speciality in Enum.GetValues<Specialities>())
            {
                SpecialitiesList.Add(new SelectListItem { Text = EnumExtensions.GetDisplayName(speciality), Value = speciality.ToString(), Selected = speciality == student.Speciality });
            }

            ImageName = student.Image;

            return Page();
        }

        #region Change Account Details
        public async Task<IActionResult> OnPostChangeAccountDetailsAsync()
        {
            try
            {
                ModelState.Remove(nameof(Image));
                ModelState.Remove(nameof(Password));

                bool emailExists = await _service.GetStudentByEmailAsync(Student.Email) != null;
                Student oldStudent = await _service.GetStudentAsync(Convert.ToInt32(HttpContext.Session.GetInt32("Id")));

                if (emailExists && oldStudent.Email != Student.Email)
                {
                    ModelState.AddModelError("Student.Email", "Email already exists.");
                }

                if (ModelState.GetFieldValidationState("Student.Name") == ModelValidationState.Valid
                    && ModelState.GetFieldValidationState("Student.Email") == ModelValidationState.Valid
                    && ModelState.GetFieldValidationState("Student.Speciality") == ModelValidationState.Valid
                    && ModelState.GetFieldValidationState("Student.EndDate") == ModelValidationState.Valid
                    && ModelState.GetFieldValidationState("Student.Description") == ModelValidationState.Valid)
                {
                    Student updatedStudent = new Student(Convert.ToInt32(HttpContext.Session.GetInt32("Id")), Student.Name, oldStudent.Image, Student.Description, Student.Email, Student.Speciality, oldStudent.ClassId, Student.EndDate, oldStudent.Password);
                    await _service.UpdateStudentAsync(updatedStudent);
                    HttpContext.Session.AddToastNotification(new ToastNotification { Message = "Account details have been changed!", Status = ToastColor.Success });
                    return Redirect("/Account");
                }
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                ModelState.AddModelError("Student", "Something went wrong");
                throw;
            }

            return await OnGetAsync();
        }
        #endregion

        #region Change Password

        [BindProperty]
        public ChangePasswordDTO Password { get; set; }

        public async Task<IActionResult> OnPostChangePasswordAsync()
        {
            try
            {
                ModelState.Remove(nameof(Image));

                Student student = await _service.GetStudentAsync(Convert.ToInt32(HttpContext.Session.GetInt32("Id")));
                bool validatePassword = BC.EnhancedVerify(Password.Old ?? String.Empty, student.Password);

                if (validatePassword)
                {
                    if (ModelState.GetFieldValidationState("Password.Old") == ModelValidationState.Valid
                    && ModelState.GetFieldValidationState("Password.New") == ModelValidationState.Valid
                    && ModelState.GetFieldValidationState("Password.Confirm") == ModelValidationState.Valid)
                    {
                        Student updatedStudent = new Student(Convert.ToInt32(HttpContext.Session.GetInt32("Id")), student.Name, student.Image, student.Description, student.Email, student.Speciality, student.ClassId, student.EndDate, BC.EnhancedHashPassword(Password.New));
                        await _service.UpdateStudentAsync(updatedStudent);
                        HttpContext.Session.AddToastNotification(new ToastNotification { Message = "Password has been changed!", Status = ToastColor.Success });
                        return Redirect("/Account");
                    }
                }
                else
                {
                    ModelState.AddModelError("Password.Old", "Wrong password");

                }
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                ModelState.AddModelError("Password", "Something went wrong");
            }

            return await OnGetAsync();
        }

        #endregion

        #region Update Profile Picture

        [BindProperty]
        [Required]
        [DataType(DataType.Upload)]
        [FileTypesAttributes(new String[] { "png", "jpg", "jpeg", "gif" })]
        public IFormFile Image { get; set; }

        public async Task<IActionResult> OnPostUpdateProfilePictureAsync()
        {
            try
            {
                if (ModelState.GetFieldValidationState("Image") == ModelValidationState.Invalid)
                {
                    return await OnGetAsync();
                }

                Guid guid = Guid.NewGuid();
                string fileName = guid.ToString() + "." + Image.ContentType.Split('/').Last();
                string filePath = Path.Combine(_environment.ContentRootPath, "wwwroot\\img\\", fileName);
                using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await Image.CopyToAsync(fileStream);
                }

                Student student = await _service.GetStudentAsync(Convert.ToInt32(HttpContext.Session.GetInt32("Id")));

                if (student.Image != null || student.Image == "default.png")
                {
                    string oldFilePath = Path.Combine(_environment.ContentRootPath, "wwwroot\\img\\", student.Image);
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }

                await _service.UpdateStudentAsync(new Student(student.Id, student.Name, fileName, student.Description, student.Email, student.Speciality, student.ClassId, student.EndDate, student.Password));
                HttpContext.Session.AddToastNotification(new ToastNotification { Message = "Profile picture has been updated!", Status = ToastColor.Success });
                return Redirect("/Account");
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                ModelState.AddModelError("Image", "Something went wrong.");
            }

            return await OnGetAsync();
        }

        #endregion

    }
}
