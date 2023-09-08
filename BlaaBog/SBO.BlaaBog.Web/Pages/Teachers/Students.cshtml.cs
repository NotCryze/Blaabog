using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.OpenApi.Extensions;
using SBO.BlaaBog.Domain.Entities;
using SBO.BlaaBog.Services.Services;
using SBO.BlaaBog.Web.DTO;
using SBO.BlaaBog.Web.Utils;

namespace SBO.BlaaBog.Web.Pages.Teachers
{
    public class StudentsModel : PageModel
    {
        private readonly StudentService _studentService;
        public StudentsModel()
        {
            _studentService = new StudentService();
        }
        public List<StudentAccountDTO> Students { get; set; } = new();
        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                List<Student>? students = await _studentService.GetStudentsAsync();

                foreach (Student student in students)
                {
                    StudentAccountDTO studentDTO = new StudentAccountDTO
                    {
                        Id = student.Id,
                        Name = student.Name,
                        Description = student.Description,
                        Email = student.Email,
                        Speciality = student.Speciality,
                        EndDate = student.EndDate,
                        SpecialitiesList = new List<SelectListItem>()
                    };
                    foreach (Specialities speciality in Enum.GetValues<Specialities>())
                    {
                        studentDTO.SpecialitiesList.Add(new SelectListItem { Text = EnumExtensions.GetDisplayName(speciality), Value = speciality.ToString(), Selected = speciality == studentDTO.Speciality });
                    }
                    Students.Add(studentDTO);
                }
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
            }

            return Page();
        }

        #region Edit Student
        [BindProperty]
        public StudentAccountDTO Student { get; set; }
        public async Task<IActionResult> OnPostEditStudentAsync(int id)
        {
            try
            {
                Student oldStudent = await _studentService.GetStudentAsync(id);
                if (oldStudent != null)
                {
                    Student updatedStudent = new Student(id, Student.Name, oldStudent.Image, Student.Description ?? "", Student.Email, Student.Speciality, oldStudent.ClassId, Student.EndDate, oldStudent.Password);
                    await _studentService.UpdateStudentAsync(updatedStudent);
                    HttpContext.Session.AddToastNotification(new ToastNotification { Message = "Student has been updated!", Status = ToastColor.Success });
                }
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                HttpContext.Session.AddToastNotification(new ToastNotification { Message = "Something went wrong!", Status = ToastColor.Danger });
            }

            return await OnGetAsync();
        }
        #endregion

        #region Delete Student

        public async Task<IActionResult> OnPostDeleteStudentAsync(int id)
        {
            try
            {
                bool success = await _studentService.DeleteStudentAsync(id);
                if (success)
                {
                    HttpContext.Session.AddToastNotification(new ToastNotification { Message = "Student has been deleted!", Status = ToastColor.Success });
                    return Redirect("/Teachers/Students");
                }
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                HttpContext.Session.AddToastNotification(new ToastNotification { Message = "Something went wrong!", Status = ToastColor.Danger });
            }

            return await OnGetAsync();
        }

        #endregion
    }
}
