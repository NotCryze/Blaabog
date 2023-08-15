using System.ComponentModel.DataAnnotations;

namespace SBO.BlaaBog.Web.DTO
{
    public class TeacherAccountDTO
    {
        [Required]
        [StringLength(100, MinimumLength = 4)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(512, MinimumLength = 4)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
