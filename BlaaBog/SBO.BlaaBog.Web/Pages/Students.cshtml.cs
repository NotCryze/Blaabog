using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Memory;
using SBO.BlaaBog.Domain.Entities;
using SBO.BlaaBog.Services.Services;
using SBO.BlaaBog.Web.DTO;
using SBO.BlaaBog.Web.Utils;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace SBO.BlaaBog.Web.Pages
{
    public class StudentsModel : PageModel
    {
        private readonly StudentService _studentService;
        private readonly CommentService _commentService;
        private readonly ClassService _classService;
        private readonly ReportService _reportService;
        private readonly IMemoryCache _cache;

        public StudentsModel(IMemoryCache cache)
        {
            _studentService = new StudentService();
            _commentService = new CommentService();
            _classService = new ClassService();
            _reportService = new ReportService();
            _cache = cache;
        }

        public Student Student { get; set; }
        public Class Class { get; set; }
        public List<Comment> Comments { get; set; } = new List<Comment>();

        #region Pagination
        public int PageItems { get; set; } = 5;
        public int CurrentPage { get; set; } = 1;
        public double TotalPage
        {
            get
            {
                return Math.Ceiling((double)Comments.Count / PageItems);
            }
        }

        public int StartPage
        {
            get
            {
                int startPage = CurrentPage < 3 ? 1 : CurrentPage - 2; // Sets the start page to 1 if the current page is less than 3, otherwise it sets it to the current page - 2
                startPage = (TotalPage - startPage) < 4 ? (int)TotalPage - 4 : startPage; // Sets the start page to the total page - 4 if the total page - start page is less than 4, otherwise it sets it to the start page
                startPage = startPage < 1 ? 1 : startPage; // Sets the start page to 1 if the start page is less than 1
                return startPage;
            }
            set { }
        }

        public int EndPage
        {
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

        public List<SelectListItem> Reasons { get; set; } = new()
        {
            new SelectListItem { Text = "Inappropriate" },
            new SelectListItem { Text = "Hurtful" },
            new SelectListItem { Text = "Spam" },
            new SelectListItem { Text = "Other", Value = "" }
        };

        public async Task<IActionResult> OnGetAsync(int id)
        {
            string? PageNumber = Request.Query["page"];
            if (PageNumber != null && Int32.TryParse(PageNumber, out int result))
            {
                CurrentPage = Math.Max(Int32.Parse(PageNumber), 1);
            }

            try
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

            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
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
                try
                {
                    if (HttpContext.Items["User"] is Student)
                    {
                        Comment comment = new Comment
                        {
                            AuthorId = Convert.ToInt32(HttpContext.Session.GetInt32("Id")),
                            SubjectId = id,
                            Content = Comment.Content
                        };
                        await _commentService.CreateCommentAsync(comment);
                        HttpContext.Session.AddToastNotification(new ToastNotification { Message = "Comment has been sent!", Status = ToastColor.Success });
                    }
                    else
                    {
                        HttpContext.Session.AddToastNotification(new ToastNotification { Message = "Only students can send comments!", Status = ToastColor.Danger });
                    }
                }
                catch (Exception ex)
                {
                    await Console.Out.WriteLineAsync(ex.Message);
                    HttpContext.Session.AddToastNotification(new ToastNotification { Message = "Something went wrong!", Status = ToastColor.Danger });
                }
            }

            return Redirect(HttpContext.Request.Path + "#comments");
        }

        #endregion

        #region Delete Comment

        public async Task<IActionResult> OnPostDeleteCommentAsync(int delId)
        {
            try
            {
                Comment comment = await _commentService.GetCommentAsync(delId);
                if (comment != null)
                {
                    await _commentService.DeleteCommentAsync(delId);
                    HttpContext.Session.AddToastNotification(new ToastNotification { Message = "Comment has been deleted!", Status = ToastColor.Success });
                }
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                HttpContext.Session.AddToastNotification(new ToastNotification { Message = "Something went wrong!", Status = ToastColor.Danger });
            }

            return Redirect(HttpContext.Request.Path + "#comments");
        }

        #endregion

        #region Report Comment

        [BindProperty]
        public Report Report { get; set; }

        [BindProperty]
        [MaxLength(250)]
        public string CustomReason { get; set; }

        public async Task<IActionResult> OnPostReportCommentAsync(int id, int comment)
        {

            Report.Reason = Report.Reason == null ? CustomReason : Report.Reason;
            Report.CommentId = comment;


            try
            {
                if (await _commentService.GetCommentAsync(comment) != null)
                {
                    await _reportService.CreateReportAsync(Report);
                    HttpContext.Session.AddToastNotification(new ToastNotification { Message = "Comment has been reported!", Status = ToastColor.Success });
                }
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                HttpContext.Session.AddToastNotification(new ToastNotification { Message = "Something went wrong!", Status = ToastColor.Danger });
            }

            return Redirect(HttpContext.Request.Path + "#comments");
        }

        #endregion


    }
}