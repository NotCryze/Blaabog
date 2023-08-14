using System.ComponentModel.DataAnnotations;

namespace SBO.BlaaBog.Web.DTO
{
    public class RegisterDTO
    {
        [Required]
        [StringLength(6, MinimumLength = 6)]
        public string Token { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 4)]
        public string Name { get; set; }

        [Required]
        [StringLength(512, MinimumLength = 4)]
        public string Password { get; set; }
    }
}
