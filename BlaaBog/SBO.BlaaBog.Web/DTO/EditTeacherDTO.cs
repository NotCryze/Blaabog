using System.ComponentModel.DataAnnotations;

namespace SBO.BlaaBog.Web.DTO
{
    public class EditTeacherDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 4)]
        public string Name { get; set; }

        [Required]
        [Range(1, 2, ErrorMessage = "Please select a valid option")]
        public bool Admin { get; set; }

        [Required]
        [StringLength(512, MinimumLength = 4)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [StringLength(512, MinimumLength = 4)]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
