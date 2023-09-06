using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SBO.BlaaBog.Domain.Connections;
using SBO.BlaaBog.Domain.Entities;
using SBO.BlaaBog.Services.Services;
using SBO.BlaaBog.Web.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SBO.BlaaBog.Web.Pages.Teachers
{
    public class TokensModel : PageModel
    {
        private readonly TeacherTokenService _teacherTokenService;
        private readonly TeacherService _teacherService;

        public TokensModel()
        {
            _teacherTokenService = new TeacherTokenService();
            _teacherService = new TeacherService();
        }

        private static readonly Random Random = new Random();
        public List<TeacherToken>? TeacherTokens { get; set; } = new List<TeacherToken>();
        public string GeneratedToken { get; private set; }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                List<TeacherToken> tokens = await _teacherTokenService.GetTeacherTokensAsync();
                foreach (var item in tokens)
                {
                    TeacherTokens.Add(
                        new TeacherToken
                        {
                            Id = item.Id,
                            Token = item.Token,
                            TeacherId = item.TeacherId,
                            Teacher = await _teacherService.GetTeacherAsync(Convert.ToInt32(item.TeacherId)),
                            CreatedAt = item.CreatedAt
                        });
                }
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (Request.Form.ContainsKey("generateToken"))
                {
                    GeneratedToken = await GenerateTokenAsync();
                    var newToken = new TeacherToken
                    {
                        Id = 0,
                        Token = GeneratedToken,
                        TeacherId = null,
                        Teacher = null
                    };

                    bool tokenCreated = await _teacherTokenService.CreateTeacherTokenAsync(newToken);

                    if (tokenCreated)
                    {
                        HttpContext.Session.AddToastNotification(new ToastNotification { Message = "Token has been generated!", Status = ToastColor.Success });
                        return RedirectToPage("/Teachers/Tokens");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Failed to create the token.");
                        return Page();
                    }
                }
                else if (Request.Form.ContainsKey("deleteToken"))
                {
                    int token = Convert.ToInt32(Request.Form["deleteToken"]);
                    bool tokenDeleted = await _teacherTokenService.DeleteTeacherTokenAsync(token);

                    if (tokenDeleted)
                    {
                        HttpContext.Session.AddToastNotification(new ToastNotification { Message = "Token has been deleted!", Status = ToastColor.Success });
                        return RedirectToPage("/Teachers/Tokens");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Failed to delete the token.");
                        return Page();
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid form submission.");
                    return Page();
                }
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                HttpContext.Session.AddToastNotification(new ToastNotification { Message = "Something went wrong!", Status = ToastColor.Danger });

            }

            return await OnGetAsync();
        }

        public async Task<string> GenerateTokenAsync(int length = 6)
        {
            const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var token = new StringBuilder();

            for (int i = 0; i < length; i++)
            {
                int index = Random.Next(characters.Length);
                token.Append(characters[index]);
            }

            return token.ToString();
        }
    }
}
