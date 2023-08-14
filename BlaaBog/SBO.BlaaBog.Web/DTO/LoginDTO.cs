using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace SBO.BlaaBog.Web.DTO
{
    public class LoginDTO
    {
        [EmailAddress]
        [StringLength (320)]
        public string? Email { get; set; }

        [StringLength (512)]
        public string? Password { get; set; }
    }
}
