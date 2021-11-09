using System;

namespace Domain.Models
{
    public class CreateCommentDto
    {
        public string CommentText { get; set; }
        public Guid BlogPostId { get; set; }
    }
}
