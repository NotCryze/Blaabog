using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SBO.BlaaBog.Domain.Entities;
using SBO.BlaaBog.Services.Services;

namespace SBO.BlaaBog.Web.Pages
{
    public class ClassesModel : PageModel
    {
        private readonly StudentService _studentService;
        private readonly ClassService _classService;
        public ClassesModel()
        {
            _studentService = new StudentService();
            _classService = new ClassService();
        }

        public List<Student> Students { get; set; }
        public Class Class { get; set; }

        public async Task<IActionResult> OnGet(int id)
        {
            Students = await _studentService.GetStudentsByClassAsync(id);
            Class = await _classService.GetClassAsync(id);

            return Page();
        }
    }
}
