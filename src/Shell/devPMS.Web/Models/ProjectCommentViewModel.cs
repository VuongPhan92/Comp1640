using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace devPMS.Web.Models
{
    public class ProjectCommentViewModel
    {
        public int CommentId { get; set; }
        public int ProjectId { get; set; }
        public int UserId { get; set; }
        [AllowHtml]
        [StringLength(4000, ErrorMessage = "The {0} have maximum {2} characters exceeded.")]
        public string Body { get; set; }
        public bool IsWatching { get; set; }
    }
}
