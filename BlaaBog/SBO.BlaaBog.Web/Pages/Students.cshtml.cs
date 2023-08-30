using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SBO.BlaaBog.Domain.Entities;
using SBO.BlaaBog.Services.Services;
using SBO.BlaaBog.Web.DTO;
using System.Reflection;

namespace SBO.BlaaBog.Web.Pages
{
    public class StudentsModel : PageModel
    {
        private readonly StudentService _studentService;
        private readonly CommentService _commentService;
        private readonly ClassService _classService;

        public StudentsModel()
        {
            _studentService = new StudentService();
            _commentService = new CommentService();
            _classService = new ClassService();
        }

        public Student Student { get; set; }
        public Class Class { get; set; }
        public List<Comment> Comments { get; set; } = new List<Comment>();

        #region Pagination
        public int PageItems { get; set; } = 5;
        public int CurrentPage { get; set; } = 1;
        public double TotalPage {
            get
            {
               return Math.Ceiling((double)Comments.Count / PageItems);
            }
        }

        public int StartPage { 
            get 
            {
                int startPage = CurrentPage < 3 ? 1 : CurrentPage - 2; // Sets the start page to 1 if the current page is less than 3, otherwise it sets it to the current page - 2
                startPage = (TotalPage - startPage) < 4 ? (int)TotalPage - 4 : startPage; // Sets the start page to the total page - 4 if the total page - start page is less than 4, otherwise it sets it to the start page
                startPage = startPage < 1 ? 1 : startPage; // Sets the start page to 1 if the start page is less than 1
                return startPage;
            }  
            set { }
        }

        public int EndPage {
            get
            {
                int endPage = StartPage + 4;
                endPage = (TotalPage < endPage) ? (int)TotalPage : endPage;
                return endPage;
            }
            set { }
        }

        public List<Comment> DisplayedComments
        {
            get
            {
                int startIndex = (CurrentPage - 1) * PageItems;
                return Comments.Skip(startIndex).Take(PageItems).ToList();
            }
        }

        #endregion

        public async Task<IActionResult> OnGetAsync(int id)
        {

            Student = await _studentService.GetStudentAsync(id);
            if (Student == null)
            {
                return Redirect("/");
            }

            Class = await _classService.GetClassAsync(Student.ClassId);
            List<Comment> comments = await _commentService.GetCommentsBySubjectAsync(id);

            foreach (Comment com in comments)
            {
                com.Author = await _studentService.GetStudentAsync(com.AuthorId);
                Comments.Add(com);
            }

            Comments = Comments.OrderByDescending(c => c.CreatedAt).ToList();

            string? PageNumber = Request.Query["page"];
            if (PageNumber != null)
            {
                CurrentPage = Math.Max(Int32.Parse(PageNumber), 1);
            }

            return Page();
        }

        #region Comment

        [BindProperty]
        public CommentDTO Comment { get; set; }
        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (ModelState.GetFieldValidationState("Comment.Content") == ModelValidationState.Valid)
            {
                Comment comment = new Comment
                {
                    AuthorId = Convert.ToInt32(HttpContext.Session.GetInt32("Id")),
                    SubjectId = id,
                    Content = Comment.Content
                };

                await _commentService.CreateCommentAsync(comment);
            }
            return Redirect(HttpContext.Request.Path + "#comments");
        }

        #endregion

        #region Delete Comment

        public async Task<IActionResult> OnPostDeleteCommentAsync(int delId)
        {
            Comment comment = await _commentService.GetCommentAsync(delId);
            if (comment != null)
            {
                await _commentService.DeleteCommentAsync(delId);
            }
            return Redirect(HttpContext.Request.Path + "#comments");
        }

        #endregion


    }
}