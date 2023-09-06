using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SBO.BlaaBog.Domain.Entities;
using SBO.BlaaBog.Services.Services;
using SBO.BlaaBog.Web.Utils;

namespace SBO.BlaaBog.Web.Pages.Teachers
{
    public class ReportsModel : PageModel
    {
        private readonly ReportService _reportService;
        private readonly CommentService _commentService;

        public ReportsModel()
        {
            _reportService = new ReportService();
            _commentService = new CommentService();
        }

        public List<Report>? Reports { get; set; } = new List<Report>();

        public async Task<IActionResult> OnGetAsync()
        {
            List<Report> reports = await _reportService.GetReportsAsync();
            foreach (var item in reports)
            {
                Reports.Add(new Report
                {
                    Id = item.Id,
                    CommentId = item.CommentId,
                    Comment = await _commentService.GetCommentAsync(Convert.ToInt32(item.CommentId)),
                    Reason = item.Reason,
                    CreatedAt = item.CreatedAt
                });
            }

            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            try
            {
                bool reportDeleted = await _reportService.DeleteReportAsync(id);
                if (reportDeleted)
                {
                    HttpContext.Session.AddToastNotification(new ToastNotification { Message = "Successfully deleted report", Status = ToastColor.Success });
                }
                else
                {
                    HttpContext.Session.AddToastNotification(new ToastNotification { Message = "Something went wrong", Status = ToastColor.Danger });
                }
            }
            catch (Exception exception)
            {
                await Console.Out.WriteLineAsync(exception.Message);
                HttpContext.Session.AddToastNotification(new ToastNotification { Message = "Something went wrong", Status = ToastColor.Danger });
            }
            return RedirectToPage("Reports");
        }

        public async Task<IActionResult> OnPostApproveAsync(int id)
        {
            try
            {
                Report report = await _reportService.GetReportAsync(id);
                bool reportDeleted = await _reportService.DeleteReportAsync(id);
                bool commentDeleted = await _commentService.DeleteCommentAsync(Convert.ToInt32(report.CommentId));
                if (reportDeleted && commentDeleted)
                {
                    HttpContext.Session.AddToastNotification(new ToastNotification { Message = "Successfully deleted comment", Status = ToastColor.Success });
                }
                else
                {
                    HttpContext.Session.AddToastNotification(new ToastNotification { Message = "Something went wrong", Status = ToastColor.Danger });
                }
            }
            catch (Exception exception)
            {
                await Console.Out.WriteLineAsync(exception.Message);
                HttpContext.Session.AddToastNotification(new ToastNotification { Message = "Something went wrong", Status = ToastColor.Danger });
            }

            return RedirectToPage("Reports");
        }
    }
}
