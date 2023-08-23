using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SBO.BlaaBog.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace SBO.BlaaBog.Web.DTO
{
    public class StudentAccountDTO
    {
        [Required]
        [StringLength(100)]
        public string? Name { get; set; }

        [StringLength(4000)]
        [DataType(DataType.MultilineText)]
        public string? Description { get; set; }

        [Required]
        [StringLength(320)]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Required]
        public Specialities? Speciality { get; set; }

        [DataType(DataType.Date)]
        public DateOnly? EndDate { get; set; }
    }
}
