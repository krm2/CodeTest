using System;

namespace Domain.Models
{
    public class CommentDto
    {
        public Guid Id { get; set; }
        public string CommentText { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }
    }
}
