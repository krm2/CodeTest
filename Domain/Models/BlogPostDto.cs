using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public class BlogPostDto
    {
        public Guid Id { get; set; }
        public string CaptionText { get; set; }
        public byte[] Image { get; set; }
        public string Owner { get; set; }
        public IEnumerable<CommentDto> Comments { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }
    }
}
