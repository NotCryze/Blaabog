using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SBO.BlaaBog.Domain.Entities;
using SBO.BlaaBog.Services.Services;
using SBO.BlaaBog.Web.Utils;
using System.ComponentModel.DataAnnotations;

namespace SBO.BlaaBog.Web.Pages.Teachers
{
    public class ClassesModel : PageModel
    {

        private readonly ClassService _classService;
        public ClassesModel()
        {
            _classService = new ClassService();
        }


        public List<Class> Classes { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            Classes = await _classService.GetClassesAsync();
            return Page();
        }

        #region Add Class

        [BindProperty]
        [Required]
        public DateOnly StartDate { get; set; }
        public async Task<IActionResult> OnPostAddClassAsync()
        {
            ModelState.Remove("EditStartDate");
            if (ModelState.GetFieldValidationState("StartDate") == ModelValidationState.Valid)
            {
                string token;
                do
                {
                    string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
                    token = "";
                    Random rnd = new Random(Guid.NewGuid().GetHashCode());
                    for (int i = 0; i < 6; i++)
                    {
                        token += chars[rnd.Next(chars.Length)];
                    }
                } while (await _classService.CheckClassTokenAsync(token) > 0);

                Class @class = new Class(null, StartDate, token);
                await _classService.CreateClassAsync(@class);
                HttpContext.Session.AddToastNotification(new ToastNotification { Message = "Class has been created!", Status = ToastColor.Success });
                return Redirect("/Teachers/Classes");
            }
            return await OnGetAsync();
        }
        #endregion

        #region Delete Class

        public async Task<IActionResult> OnPostDeleteClassAsync(int id)
        {
            ModelState.Clear();

            if (await _classService.DeleteClassAsync(id))
            {
                HttpContext.Session.AddToastNotification(new ToastNotification { Message = "Class has been deleted!", Status = ToastColor.Success });
                return Redirect("/Teachers/Classes");
            }

            return await OnGetAsync();
        }

        #endregion

        #region Edit Class

        [BindProperty]
        [Required]
        public DateOnly EditStartDate { get; set; }

        public async Task<IActionResult> OnPostEditClassAsync(int id)
        {
            ModelState.Remove("StartDate");
            if (ModelState.GetFieldValidationState("EditStartDate") == ModelValidationState.Valid)
            {
                Class? @class = await _classService.GetClassAsync(id);
                if (@class != null)
                {
                    Class updatedClass = new Class(@class.Id, EditStartDate, @class.Token);

                    await _classService.UpdateClassAsync(updatedClass);
                    HttpContext.Session.AddToastNotification(new ToastNotification { Message = "Class has been updated!", Status = ToastColor.Success });

                    return Redirect("/Teachers/Classes");
                }
            }
            return await OnGetAsync();
        }

        #endregion


    }
}
