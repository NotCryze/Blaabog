using System.ComponentModel.DataAnnotations;

namespace SBO.BlaaBog.Web.DTO
{
    public class ChangePasswordDTO
    {
        [Required]
        [StringLength(512, MinimumLength = 4)]
        [DataType(DataType.Password)]
        public string Old { get; set; }

        [Required]
        [StringLength(512, MinimumLength = 4)]
        [DataType(DataType.Password)]
        public string New { get; set; }

        [Required]
        [StringLength(512, MinimumLength = 4)]
        [DataType(DataType.Password)]
        [Compare("New", ErrorMessage = "Passwords do not match!")]
        public string Confirm { get; set; }
    }
}
