using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SBO.BlaaBog.Domain.Entities;
using SBO.BlaaBog.Services.Services;
using System.Reflection;
using System.Runtime.InteropServices;

namespace SBO.BlaaBog.Web.Pages.Teachers
{
    public class IndexModel : PageModel
    {
        private readonly ClassService _classService;
        private readonly CommentService _commentService;
        private readonly StudentService _studentService;
        private readonly TeacherService _teacherService;
        
        public IndexModel()
        {
            _classService = new ClassService();
            _commentService = new CommentService();
            _studentService = new StudentService();
            _teacherService = new TeacherService();
        }
        
        public int TotalClasses { get; set; }
        public int TotalStudents { get; set; }
        public int TotalComments { get; set; }
        public int NewComments { get; set; }

        public List<DateOnly> ChartX { get; set; }
        public List<int> ChartY { get; set; }

        public List<Class> LatestClasses { get; set; }
        public List<Student> LatestStudents { get; set; }

        public Dictionary<Specialities, int> PieChartData { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            TotalClasses = await _classService.GetClassesCountAsync();
            TotalStudents = await _studentService.GetStudentsCountAsync();
            TotalComments = await _commentService.GetCommentsCountAsync();
            NewComments = await _commentService.GetNewCommentsCountAsync();

            Dictionary<DateOnly, int> comments = await _commentService.GetCommentsGroupedByMonthAsync();
            ChartX = comments.Keys.ToList();
            ChartY = comments.Values.ToList();

            PieChartData = await _studentService.GetStudentsCountGroupedBySpecialityAsync();

            LatestClasses = await _classService.GetLatestClassesAsync(5);
            LatestStudents = await _studentService.GetLatestStudentsAsync(5);

            return Page();
        }

        public async Task<JsonResult> OnGetComments()
        {
            Dictionary<DateOnly, int> comments = await _commentService.GetCommentsGroupedByMonthAsync();

            Dictionary<string, Object> response = new Dictionary<string, Object>();
            response.Add("x", comments.Keys.ToList());
            response.Add("y", comments.Values.ToList());

            return new JsonResult(response);
        }
    }
}
