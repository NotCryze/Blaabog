using Microsoft.AspNetCore.Mvc;
using SBO.BlaaBog.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace SBO.BlaaBog.Web.DTO
{
    public class ReportDTO
    {
        public int? Id { get; set; }

        public int CommentId { get; set; }

        public Comment Comment { get; set; }


        [Required(ErrorMessage = "Reason is required")]
        public string Reason { get; set; }

        public DateTime CreatedAt { get; set; }

        [MaxLength(250)]
        public string CustomReason { get; set; }

    }
}