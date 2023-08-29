using SBO.BlaaBog.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace SBO.BlaaBog.Web.DTO
{
    public class CommentDTO
    {
        public int? Id { get; set; }
        public int AuthorId { get; set; }
        public Student Author { get; set; }
        public int SubjectId { get; set; }
        public Student Subject { get; set; }

        [MaxLength(500)]
        [Required]
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
