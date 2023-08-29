using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SBO.BlaaBog.Domain.Connections;
using SBO.BlaaBog.Domain.Entities;
using SBO.BlaaBog.Services.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SBO.BlaaBog.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ClassService _classService;

        private readonly StudentService _studentService;

        public IndexModel()
        {
            _classService = new ClassService();
            _studentService = new StudentService();
        }

        public List<Class> Classes { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Classes = await _classService.GetClassesAsync();
            foreach (var @class in Classes)
            {
                @class.Students = await _studentService.GetStudentsByClassAsync(Convert.ToInt32(@class.Id));
            }
            return Page();
        }

    }
}
